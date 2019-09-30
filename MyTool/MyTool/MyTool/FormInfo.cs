using System.Collections.Generic;
using System.Windows.Forms;

//自建类，用于进行窗体管理
namespace FormInfo
{
    public partial class PLCSumCheck
    {

        //创建全局变量，用来存放已show出的窗体对象
        List<Form> AllForms = new List<Form>();
        /// <summary>
        /// 显示唯一的窗体
        /// </summary>
        /// <param name="Form"></param>
        private void OnlySubform(Form Form)
        {
            //判断窗体是否已经弹出，默认false
            bool hasform = false;
            //遍历所有窗体对象
            foreach (Form f in AllForms)
            {
                //判断弹出的窗体是否重复
                if (f.Name == Form.Name)
                {
                    //重复，修改为true
                    hasform = true;
                    f.WindowState = FormWindowState.Normal;
                    //获取焦点
                    f.Focus();
                }
            }
            if (hasform)
            {
                Form.Hide();
            }
            else
            {
                //添加到所有窗体中
                AllForms.Add(Form);
                //并打开该窗体
                Form.Show();
            }
        }
        /// <summary>
        /// 关闭时清除窗体信息
        /// </summary>
        /// <param name="Form"></param>
        private void CleanTheform(Form Form)
        {
            //遍历所有窗体对象
            foreach (Form f in AllForms)
            {
                //判断弹出的窗体是否重复
                if (f.Name == Form.Name)
                {
                    AllForms.Remove(Form);
                }
            }

        }
    }
}