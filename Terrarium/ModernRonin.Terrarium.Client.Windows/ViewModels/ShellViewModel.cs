﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using ModernRonin.Terrarium.Logic;
using ModernRonin.Terrarium.Rendering.Windows.Interaction;

namespace ModernRonin.Terrarium.Client.Windows.ViewModels
{
    public class ShellViewModel : Screen, IDisposable
    {
        readonly ISimulation mSimulation;
        readonly IPicker mPicker;
        string mToggleRunText = "Start";
        public ShellViewModel(ISimulation simulation, Action<SwapChainPanel> setupView, IPicker picker)
        {
            SetupView = setupView;
            mSimulation = simulation;
            mPicker = picker;
            mPicker.OnEntitiesPicked += OnEntitiesPicked;
        }
        
        void OnEntitiesPicked(IEnumerable<Entity> entities)
        {
            var frozen = entities.ToArray();
            if (frozen.Any())
                Debug.WriteLine(string.Join("\r\n", frozen.Select(e => e.Code)));
        }
        public Action<SwapChainPanel> SetupView { get; }
        public string ToggleRunText
        {
            get => mToggleRunText;
            set
            {
                if (value == mToggleRunText) return;
                mToggleRunText = value;
                NotifyOfPropertyChange(() => ToggleRunText);
            }
        }
        public void ExitApplication()
        {
            Application.Current.Exit();
        }
        public async Task ToggleRun()
        {
            if (mSimulation.IsRunning)
            {
                await mSimulation.Stop();
                ToggleRunText = "Start";
            }
            else
            {
                mSimulation.Start();
                ToggleRunText = "Stop";
            }
        }
        public void Dispose()
        {
            mPicker.OnEntitiesPicked -= OnEntitiesPicked;
        }
    }
}