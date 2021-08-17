﻿using SimpleTraderApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTraderApp.WPF.ViewModels.Factories
{
    public class SimpleTradeViewModelFactory : ISimpleTradeViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _CreateHomeViewModel;
        private readonly CreateViewModel<PortfolioViewModel> _CreatePortfolioViewModel;
        private readonly CreateViewModel<LoginViewModel> _CreateLoginViewModel;
        private readonly CreateViewModel<BuyViewModel> _CreateBuyViewModel;

        public SimpleTradeViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel, 
                                            CreateViewModel<PortfolioViewModel> createPortfolioViewModel, 
                                            CreateViewModel<LoginViewModel> createLoginViewModel, 
                                            CreateViewModel<BuyViewModel> createBuyViewModel)
        {
            _CreateHomeViewModel = createHomeViewModel;
            _CreatePortfolioViewModel = createPortfolioViewModel;
            _CreateLoginViewModel = createLoginViewModel;
            _CreateBuyViewModel = createBuyViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _CreateLoginViewModel();
                case ViewType.Home:
                    return _CreateHomeViewModel();
                case ViewType.Portfolio:
                    return _CreatePortfolioViewModel();
                case ViewType.Buy:
                    return _CreateBuyViewModel();
                default:
                    throw new ArgumentException("View Type does not have View Model", "View Type");
            }
        }
    }
}
