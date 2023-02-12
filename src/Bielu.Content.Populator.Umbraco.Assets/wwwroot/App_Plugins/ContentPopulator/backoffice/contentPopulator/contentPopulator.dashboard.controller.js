
(function () {
    'use strict';

    function dashboardController($controller,
                                 $scope, $timeout, navigationService, eventsService, uSync8DashboardService) {

        var vm = this;

        vm.selectNavigationItem = function (item) {
            eventsService.emit('usync-dashboard.tab.change', item);
        }

        vm.page = {
            title: 'uSync',
            description: '...',
            navigation: [
                {
                    'name': 'Content Populator',
                    'alias': 'contentPopulator',
                    'icon': 'icon-infinity',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/contentPopulator/panels/default.html',
                    'active': true
                },
                {
                    'name': 'Settings',
                    'alias': 'settings',
                    'icon': 'icon-settings',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/contentPopulator/panels/settings.html'
                },
                {
                    'name': 'Report',
                    'alias': 'report',
                    'icon': 'icon-settings',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/contentPopulator/panels/report.html'
                }
            ]
        };

        $timeout(function () {
            navigationService.syncTree({ tree: "contentPopulator", path: "-1" });
        });

    }

    angular.module('umbraco')
        .controller('uSyncSettingsDashboardController', dashboardController);
})();