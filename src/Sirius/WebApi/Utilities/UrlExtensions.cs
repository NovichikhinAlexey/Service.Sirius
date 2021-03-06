﻿using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.DepositWallets;
using Sirius.WebApi.Models.HotWallets;
using Sirius.WebApi.Models.Networks;

namespace Sirius.WebApi.Utilities
{
    public static class UrlExtensions
    {
        public static string AssetsUrl(this IUrlHelper url, string blockchainId, string networkId)
        {
            return url.Action(nameof(AssetsController.GetAssets),
                ControllerHelper.GetShortName<AssetsController>(),
                new { BlockchainId = blockchainId, NetworkId = networkId });
        }

        public static string DepositWalletsUrl(this IUrlHelper url, string blockchainId, string networkId, string groupName)
        {
            return url.Action(nameof(DepositWalletsController.GetDepositWallets),
                ControllerHelper.GetShortName<DepositWalletsController>(), new
                {
                    BlockchainId = blockchainId,
                    NetworkId = networkId,
                    DepositWalletRequest = new DepositWalletsRequest { GroupName = groupName }
                });
        }

        public static string HotWalletsUrl(this IUrlHelper url, string blockchainId, string networkId, string groupName)
        {
            return url.Action(nameof(HotWalletsController.GetHotWallets),
                ControllerHelper.GetShortName<HotWalletsController>(),
                new { BlockchainId = blockchainId, NetworkId = networkId, GroupName = groupName });
        }

        public static string NetworksUrl(this IUrlHelper url, string blockchainId)
        {
            return url.Action(nameof(NetworksController.GetNetworks),
                ControllerHelper.GetShortName<NetworksController>(),
                new NetworksRequest { BlockchainId = blockchainId });
        }
    }
}
