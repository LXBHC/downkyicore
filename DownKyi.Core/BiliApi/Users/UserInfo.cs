﻿using DownKyi.Core.BiliApi.Sign;
using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using Console = DownKyi.Core.Utils.Debugging.Console;

namespace DownKyi.Core.BiliApi.Users;

/// <summary>
/// 用户基本信息
/// </summary>
public static class UserInfo
{
    /// <summary>
    /// 导航栏用户信息
    /// </summary>
    /// <returns></returns>
    public static UserInfoForNavigation? GetUserInfoForNavigation()
    {
        const string url = "https://api.bilibili.com/x/web-interface/nav";
        const string referer = "https://www.bilibili.com";
        var response = WebClient.RequestWeb(url, referer);

        try
        {
            var userInfo = JsonConvert.DeserializeObject<UserInfoForNavigationOrigin>(response);

            return userInfo?.Data;
        }
        catch (Exception e)
        {
            Console.PrintLine("GetUserInfoForNavigation()发生异常: {0}", e);
            LogManager.Error("UserInfo", e);
            return null;
        }
    }

    /// <summary>
    /// 用户空间详细信息
    /// </summary>
    /// <param name="mid"></param>
    /// <returns></returns>
    public static UserInfoForSpace? GetUserInfoForSpace(long mid)
    {
        var parameters = new Dictionary<string, object>
        {
            { "mid", mid }
        };
        var query = WbiSign.ParametersToQuery(WbiSign.EncodeWbi(parameters));
        var url = $"https://api.bilibili.com/x/space/wbi/acc/info?{query}";
        const string referer = "https://www.bilibili.com";
        var response = WebClient.RequestWeb(url, referer, needRandomBvuid3: true);

        try
        {
            var spaceInfo = JsonConvert.DeserializeObject<UserInfoForSpaceOrigin>(response);

            return spaceInfo?.Data;
        }
        catch (Exception e)
        {
            Console.PrintLine("GetInfoForSpace()发生异常: {0}", e);
            LogManager.Error("UserInfo", e);
            return null;
        }
    }

    /// <summary>
    /// 本用户详细信息
    /// </summary>
    /// <returns></returns>
    public static MyInfo? GetMyInfo()
    {
        const string url = "https://api.bilibili.com/x/space/myinfo";
        const string referer = "https://www.bilibili.com";
        var response = WebClient.RequestWeb(url, referer);

        try
        {
            var myInfo = JsonConvert.DeserializeObject<MyInfoOrigin>(response);

            return myInfo?.Data;
        }
        catch (Exception e)
        {
            Console.PrintLine("GetMyInfo()发生异常: {0}", e);
            LogManager.Error("UserInfo", e);
            return null;
        }
    }
}