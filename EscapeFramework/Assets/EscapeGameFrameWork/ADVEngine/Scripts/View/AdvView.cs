using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;
using UniRx.Async;
using UniRx.Triggers;
using System.Linq;
using Qitz.ArchitectureCore.ADVGame;

namespace Qitz.ADVGame
{
    public  abstract class  AAdvView:ADVGameView{
        public abstract void Next(string jumpTo);
        public abstract IObservable<List<int>> ASVScenarioEndObservable { get; }
        public abstract IObservable<ICutVO> ADVCutObservable { get; }
    }
    
    public class AdvView : AAdvView
    {
        [SerializeField] private ABackgroundView _backgroundView;
        [SerializeField] private ACharactersWrapView _charactersWrapView;
        [SerializeField] private AWindowView _windowView;
        [SerializeField] private Transform _choiceSelectViewEmitter;
        [SerializeField] private ChoiceSelectView choiceSelectViewPrefab;
        ChoiceSelectView currentChoiseSelectView;
        [SerializeField] private ADVAudioPlayer aDVAudioPlayer;
        [SerializeField] private EffectView effectView;
        public override IObservable<List<int>> ASVScenarioEndObservable => this.aDVGameController.ASVScenarioEndObservable;
        public override IObservable<ICutVO> ADVCutObservable => this.aDVGameController.ADVCutObservable;
        ICutVO currentCut;


        public async override void Next(string jumpTo = "")
        {
            if (_windowView.IsTyping)
            {
                _windowView.ShowAllText();
                return;
            }
            await this.UpdateAsObservable().Where(_ => _charactersWrapView.CharacterViews.All(cv => !cv.IsAnimating)).Take(1);
            string _jumpTo = jumpTo;
            bool ableToJump = currentCut != null && currentCut.JumpToValue != "";
            if (ableToJump) _jumpTo = currentCut.JumpToValue;
            this.aDVGameController.Next(_jumpTo);
        }

        private async void Start()
        {
            ADVCutObservable.Subscribe(cutVO => UpdateADVViews(cutVO)).AddTo(this.gameObject);
            effectView.BlackOutEndObservable.Subscribe(_ => Next());
            Next();
            //ブラックアウトが走った場合は次のCutへ
        }

        void UpdateADVViews(ICutVO cutVo)
        {
            currentCut = cutVo;

            //画面エフェクトの実行
            cutVo.Commands.ForEach(cd => effectView.DoEffect(cd));
            //音楽を鳴らす
            aDVAudioPlayer.PlayAudio(cutVo.QitzAudio?.Audio);
            //seを鳴らす
            aDVAudioPlayer.PlaySE(cutVo.SE?.Audio);
            //Windowの表示更新
            _windowView.SetWindowVO(cutVo.WindowVO);
            //バックグラウンドの更新
            _backgroundView.SetBackgroundVO(cutVo.BackgroundVO);
            //キャラクタービューの更新
            _charactersWrapView.SetCaracterVO(cutVo.Caracters);
            //選択肢の表示
            SetChoiceView(cutVo);
        }

        void SetChoiceView(ICutVO cutVo)
        {
            bool existSelectCommand = cutVo.Commands.FirstOrDefault(cd => cd.CommandHeadVO.CommandType == CommandType.SELECT) != null;
            if (existSelectCommand)
            {
                var salAddList = cutVo.Commands.Where(cd => cd.CommandHeadVO.CommandType == CommandType.SELADD).ToList();
                currentChoiseSelectView = PrefabFolder.InstantiateTo<ChoiceSelectView>(choiceSelectViewPrefab,_choiceSelectViewEmitter);
                currentChoiseSelectView.Initialize(ChoiceSelectAction, salAddList);
            }
            //else
            //{
            //    _choiceSelectView.HideImmediately();
            //}
        }

        async void ChoiceSelectAction(string selectValue)
        {
            var selectItem = currentChoiseSelectView.SelectItems.FirstOrDefault(si=>si.IsSelected);
            int selectedNumber = currentChoiseSelectView.SelectItems.IndexOf(selectItem);
            this.aDVGameController.AddSelect(selectedNumber);
            await currentChoiseSelectView.HideView();
            //選択肢選択後はシナリオスクリプトをリロードする

            Next(selectValue);
        }


    }
}
