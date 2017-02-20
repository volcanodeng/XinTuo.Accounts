using Orchard.UI.Resources;

namespace XinTuo.Accounts
{
    /// <summary>
    /// 资源清单必须配合action的theme标签才有效
    /// </summary>
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            //manifest.DefineScript("Easyui.min").SetUrl("~/themes/sintoacct.easyui/scripts/jquery.easyui.min.js").SetDependencies("jQuery");
            //manifest.DefineScript("Easyui.lang").SetUrl("~/themes/sintoacct.easyui/scripts/easyui-lang-zh_CN.js").SetDependencies("Easyui.min");
            //manifest.DefineScript("Easyui.extend").SetUrl("~/themes/sintoacct.easyui/scripts/easyui.extend.js").SetDependencies("Easyui.lang");
            //manifest.DefineScript("Easyui").SetUrl("~/themes/sintoacct.easyui/scripts/common.js").SetDependencies("Easyui.extend");

            //manifest.DefineStyle("site").SetUrl("~/themes/sintoacct.easyui/styles/site.css");
            //manifest.DefineStyle("Easyui").SetUrl("~/themes/sintoacct.easyui/styles/easyui.css");
            //manifest.DefineStyle("Easyui.icon").SetUrl("~/themes/sintoacct.easyui/styles/icon.css");

            manifest.DefineStyle("voucher").SetUrl("voucher.css");

            manifest.DefineScript("voucher").SetUrl("Voucher.js");
        }
    }
}
