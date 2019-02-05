using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;
using Microsoft.SharePoint.Utilities;

namespace SPDataGridWP.Features.SPDataGrid_Feature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("8073276c-c1c9-4eed-b902-8764145f24dd")]
    public class SPDataGrid_FeatureEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPSite siteCollection = properties.Feature.Parent as SPSite;
                if (siteCollection != null)
                {
                    SPWeb site = siteCollection.RootWeb;

                    SPNavigationNodeCollection topNav = site.Navigation.TopNavigationBar;
                    topNav.AddAsLast(new SPNavigationNode("DataGrid-Updated", "SitePages/DataGridPage.aspx"));
                }
            }
            catch (Exception ex)
            {
                SPUtility.LogCustomAppError(ex.Message + "\n" + ex.StackTrace);
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPSite siteCollection = properties.Feature.Parent as SPSite;
                if (siteCollection != null)
                {
                    SPWeb site = siteCollection.RootWeb;
                    SPNavigationNodeCollection topNav = site.Navigation.TopNavigationBar;
                    for (int i = topNav.Count - 1; i >= 0; i--)
                    {
                        if (topNav[i].Url.Contains("DataGridPage"))
                        {
                            topNav[i].Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPUtility.LogCustomAppError(ex.Message + "\n" + ex.StackTrace);
            }
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
