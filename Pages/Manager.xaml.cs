﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using WechatBakTool.Export;
using WechatBakTool.Model;
using WechatBakTool.ViewModel;

namespace WechatBakTool.Pages
{
    /// <summary>
    /// Manager.xaml 的交互逻辑
    /// </summary>
    public partial class Manager : Page
    {
        private WorkspaceViewModel workspaceViewModel = new WorkspaceViewModel();
        public WXUserReader? UserReader;
        private List<WXContact>? ExpContacts;
        private bool Suspend = false;
        private int Status = 0;
        public Manager()
        {
            DataContext = workspaceViewModel;
            InitializeComponent();
            UserBakConfig? config = Main2.CurrentUserBakConfig;
            if (config != null)
            {
                UserReader = new WXUserReader(config);
                if (!config.Decrypt)
                {
                    MessageBox.Show("请先解密数据库", "错误");
                    return;
                }
            }
        }

        private void btn_export_all_Click(object sender, RoutedEventArgs e)
        {
            // 0 未开始
            if(Status == 0 || Status == 2)
            {
                Suspend = false;
                btn_export_all.Content = "暂停";
            }
            // 1 进行中
            else if (Status == 1)
            {
                // 开启暂停
                Suspend = true;
                Status = 2;
                btn_export_all.Content = "继续";
                return;
            }
            Task.Run(() =>
            {
                bool group = false, user = false;
                Dispatcher.Invoke(() =>
                {
                    if (cb_group.IsChecked == null || cb_user.IsChecked == null)
                        return;

                    group = (bool)cb_group.IsChecked;
                    user = (bool)cb_user.IsChecked;
                });
                if (UserReader != null)
                {
                    if (Status == 0)
                        ExpContacts = UserReader.GetWXContacts().ToList();
                    else
                        Suspend = false;

                    List<WXContact> process = new List<WXContact>();
                    foreach (var contact in ExpContacts!)
                    {
                        
                        if (Suspend)
                        {
                            foreach(WXContact p in process)
                            {
                                ExpContacts.Remove(p);
                            }
                            workspaceViewModel.ExportCount = "已暂停";
                            return;
                        }

                        Status = 1;
                        if (group && contact.UserName.Contains("@chatroom"))
                        {
                            workspaceViewModel.WXContact = contact;
                            ExportMsg(contact);
                        }
                        if (user && !contact.UserName.Contains("@chatroom") && !contact.UserName.Contains("gh_"))
                        {
                            workspaceViewModel.WXContact = contact;
                            ExportMsg(contact);
                        }
                        process.Add(contact);
                    }
                    Status = 0;
                    btn_export_all.Content = "导出";
                    MessageBox.Show("批量导出完成", "提示");
                }
            });
        }

        private void ExportMsg(WXContact contact)
        {
            workspaceViewModel.ExportCount = "";
            // string path = Path.Combine(Main2.CurrentUserBakConfig!.UserWorkspacePath, contact.UserName + ".html");
            string name = contact.NickName;
            name = name.Replace(@"\", "");
            name = Regex.Replace(name, "[ \\[ \\] \\^ \\-_*×――(^)$%~!/@#$…&%￥—+=<>《》|!！??？:：•`·、。，；,.;\"‘’“”-]", "");
            string path = Path.Combine(
                Main2.CurrentUserBakConfig!.UserWorkspacePath,
                string.Format(
                    "{0}-{1}.html",
                    contact.UserName,
                    contact.Remark == "" ? name : contact.Remark
                )
            );

            IExport export = new HtmlExport();
            export.InitTemplate(contact, path);
            DatetimePickerViewModel dt = new DatetimePickerViewModel();
            dt.DateType = 1;
            if (export.SetMsg(UserReader!, contact, workspaceViewModel, dt))
            {
                export.SetEnd();
                export.Save(path);
            }
            
        }

        private void btn_emoji_download_Click(object sender, RoutedEventArgs e)
        {
            if (UserReader != null)
            {
                Task.Run(() =>
                {
                    UserReader.PreDownloadEmoji();
                    MessageBox.Show("所有表情预下载完毕");
                });
            }
        }

        private void btn_analyse_Click(object sender, RoutedEventArgs e)
        {
            if (UserReader == null || Main2.CurrentUserBakConfig == null)
            {
                MessageBox.Show("请先读取数据");
                return;
            }
            Analyse analyse = new Analyse(Main2.CurrentUserBakConfig, UserReader);
            analyse.Show();
        }
    }
}
