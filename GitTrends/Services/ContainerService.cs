﻿using System;
using Autofac;
using GitTrends.Shared;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace GitTrends
{
    public static class ContainerService
    {
        readonly static Lazy<IContainer> _containerHolder = new Lazy<IContainer>(CreateContainer);

        public static IContainer Container => _containerHolder.Value;

        static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            //Register Xamarin.Essentials
            builder.RegisterType<AppInfoImplementation>().As<IAppInfo>().SingleInstance();
            builder.RegisterType<BrowserImplementation>().As<IBrowser>().SingleInstance();
            builder.RegisterType<EmailImplementation>().As<IEmail>().SingleInstance();
            builder.RegisterType<LauncherImplementation>().As<ILauncher>().SingleInstance();
            builder.RegisterType<MainThreadImplementation>().As<IMainThread>().SingleInstance();
            builder.RegisterType<PreferencesImplementation>().As<IPreferences>().SingleInstance();
            builder.RegisterType<SecureStorageImplementation>().As<ISecureStorage>().SingleInstance();
            builder.RegisterType<VersionTrackingImplementation>().As<IVersionTracking>().SingleInstance();

            //Register Services
            builder.RegisterType<AnalyticsService>().As<IAnalyticsService>().SingleInstance();
            builder.RegisterType<AzureFunctionsApiService>().AsSelf().SingleInstance();
            builder.RegisterType<BackgroundFetchService>().AsSelf().SingleInstance();
            builder.RegisterType<DeepLinkingService>().AsSelf().SingleInstance();
            builder.RegisterType<FavIconService>().AsSelf().SingleInstance();
            builder.RegisterType<FirstRunService>().AsSelf().SingleInstance();
            builder.RegisterType<GitHubApiV3Service>().AsSelf().SingleInstance();
            builder.RegisterType<GitHubAuthenticationService>().AsSelf().SingleInstance();
            builder.RegisterType<GitHubUserService>().AsSelf().SingleInstance();
            builder.RegisterType<GitHubGraphQLApiService>().AsSelf().SingleInstance();
            builder.RegisterType<MediaElementService>().AsSelf().SingleInstance();
            builder.RegisterType<NotificationService>().AsSelf().SingleInstance();
            builder.RegisterType<ReferringSitesDatabase>().AsSelf().SingleInstance();
            builder.RegisterType<RepositoryDatabase>().AsSelf().SingleInstance();
            builder.RegisterType<ReviewService>().AsSelf().SingleInstance();
            builder.RegisterType<SortingService>().AsSelf().SingleInstance();
            builder.RegisterType<SyncFusionService>().AsSelf().SingleInstance();
            builder.RegisterType<ThemeService>().AsSelf().SingleInstance();
            builder.RegisterType<TrendsChartSettingsService>().AsSelf().SingleInstance();
#if !AppStore
            builder.RegisterType<UITestBackdoorService>().AsSelf().SingleInstance();
#endif

            //Register ViewModels
            builder.RegisterType<OnboardingViewModel>().AsSelf();
            builder.RegisterType<ReferringSitesViewModel>().AsSelf();
            builder.RegisterType<RepositoryViewModel>().AsSelf();
            builder.RegisterType<SettingsViewModel>().AsSelf();
            builder.RegisterType<SplashScreenViewModel>().AsSelf();
            builder.RegisterType<TrendsViewModel>().AsSelf();
            builder.RegisterType<WelcomeViewModel>().AsSelf();

            //Register Pages
            builder.RegisterType<ChartOnboardingPage>().AsSelf();
            builder.RegisterType<ConnectToGitHubOnboardingPage>().AsSelf();
            builder.RegisterType<GitTrendsOnboardingPage>().AsSelf();
            builder.RegisterType<NotificationsOnboardingPage>().AsSelf();
            builder.RegisterType<OnboardingCarouselPage>().AsSelf();
            builder.RegisterType<ReferringSitesPage>().AsSelf().WithParameter(new TypedParameter(typeof(Repository), nameof(Repository).ToLower()));
            builder.RegisterType<RepositoryPage>().AsSelf();
            builder.RegisterType<SettingsPage>().AsSelf();
            builder.RegisterType<SplashScreenPage>().AsSelf();
            builder.RegisterType<TrendsPage>().AsSelf().WithParameter(new TypedParameter(typeof(Repository), nameof(Repository).ToLower()));
            builder.RegisterType<WelcomePage>().AsSelf();

            return builder.Build();
        }
    }
}
