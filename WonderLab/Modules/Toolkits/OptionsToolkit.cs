﻿using MinecraftLaunch.Modules.Toolkits;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WonderLab.Modules.Const;

namespace WonderLab.Modules.Toolkits
{
    public static class OptionsToolkit
    {
        /// <summary>
        /// 游戏语言切换
        /// </summary>
        /// <returns></returns>
        public static async ValueTask GameLangChange(int index = 1)
        {
            string langtype = "en_us";

            if (index is 0)
                langtype = "zh_cn";
            else if (index is 2) langtype = "ja_jp";
            else if (index is 3) langtype = "ko_kr";
            //ja_jp
            string path = IsEnableIndependencyCore ? Path.Combine(PathConst.GetVersionFolder(App.Data.FooterPath, App.Data.SelectedGameCore!), "options.txt") :
                Path.Combine(App.Data.FooterPath, "options.txt");

            if (!File.Exists(path))
            {
                File.WriteAllText(path, langtype);
                return;
            }

            var allText = await File.ReadAllTextAsync(path);

            foreach (var i in allText.Split("\r\n"))
            {
                if (i.Contains("lang:"))
                {
                    LogToolkit.WriteLine("发现语言节点！");
                    allText = allText.Replace(i, $"lang:{langtype}");
                    LogToolkit.WriteLine(allText);
                    File.WriteAllText(path, allText);
                    return;
                }
            }
        }

        public static bool IsEnableIndependencyCore
        {
            get
            {
                if (JsonToolkit.GetEnableIndependencyCoreData(App.Data.FooterPath, App.Data.SelectedGameCore).IsEnableIndependencyCore)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
