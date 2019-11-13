﻿using LDDModder.BrickEditor.Settings;
using LDDModder.Modding.Editing;
using System;
using System.IO;
using System.Windows.Forms;

namespace LDDModder.BrickEditor.UI.Windows
{
    public partial class BrickEditorWindow
    {

        public void UpdateMenuItemStates()
        {
            File_SaveMenu.Enabled = ProjectManager.IsProjectOpen;
            File_SaveAsMenu.Enabled = ProjectManager.IsProjectOpen;
            File_CloseProjectMenu.Enabled = ProjectManager.IsProjectOpen;
            Edit_ImportMeshMenu.Enabled = ProjectManager.IsProjectOpen;
            Edit_ValidatePartMenu.Enabled = ProjectManager.IsProjectOpen;
            Edit_GenerateFilesMenu.Enabled = ProjectManager.IsProjectOpen;

        }

        #region Main menu

        private void LDDEnvironmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new LddEnvironmentConfigWindow())
                dlg.ShowDialog();
        }

        private void ExportBrickMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new ModelImportExportWindow())
                frm.ShowDialog();
        }

        #endregion

        #region File Menu

        private void File_NewProjectMenu_Click(object sender, EventArgs e)
        {
            var project = PartProject.CreateEmptyProject();
            LoadNewPartProject(project);
        }

        private void File_CreateFromBrickMenu_Click(object sender, EventArgs e)
        {
            using (var dlg = new SelectBrickDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var selectedBrick = dlg.SelectedBrick;
                    var project = PartProject.CreateFromLddPart(selectedBrick.PartId);
                    LoadNewPartProject(project);
                }
            }
        }

        private void File_OpenProjectMenu_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (!string.IsNullOrEmpty(SettingsManager.Current.ProjectWorkspace) &&
                    Directory.Exists(SettingsManager.Current.ProjectWorkspace))
                {
                    ofd.InitialDirectory = SettingsManager.Current.ProjectWorkspace;
                }

                ofd.Filter = "LDD Part Project (*.lpp)|*.lpp";
                if (ofd.ShowDialog() == DialogResult.OK)
                    OpenPartProjectFile(ofd.FileName);
            }
        }

        private void RebuildRecentFilesMenu()
        {
            File_OpenRecentMenu.DropDownItems.Clear();

            foreach (var recentProject in SettingsManager.Current.RecentProjectFiles.ToArray())
            {
                if (!File.Exists(recentProject.ProjectFile))
                {
                    SettingsManager.Current.RecentProjectFiles.Remove(recentProject);
                    continue;
                }
                string projectName = string.Empty;

                if (recentProject.PartID > 0)
                    projectName = recentProject.PartID.ToString();
                else
                    projectName = Path.GetFileName(recentProject.ProjectFile);

                if (!string.IsNullOrEmpty(recentProject.PartName))
                    projectName += $" - {recentProject.PartName}";

                var recentFileMenuEntry = File_OpenRecentMenu.DropDownItems.Add(projectName);
                recentFileMenuEntry.Tag = recentProject;
                recentFileMenuEntry.Click += RecentFileMenuEntry_Click;
            }

            File_OpenRecentMenu.Enabled = File_OpenRecentMenu.DropDownItems.Count > 0;
        }

        private void RecentFileMenuEntry_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripItem;
            string filePath = (menuItem.Tag as RecentFileInfo).ProjectFile;
            OpenPartProjectFile(filePath);
        }


        private void File_SaveMenu_Click(object sender, EventArgs e)
        {
            if (CurrentProject != null)
                SaveProject(CurrentProject, false);
        }

        private void File_SaveAsMenu_Click(object sender, EventArgs e)
        {
            if (CurrentProject != null)
                SaveProject(CurrentProject, true);
        }

        private void File_CloseProjectMenu_Click(object sender, EventArgs e)
        {
            CloseCurrentProject();
        }

        #endregion

        private void Edit_ImportMeshMenu_Click(object sender, EventArgs e)
        {
            ImportGeometry(null);
        }

        private void Edit_GenerateFilesMenu_Click(object sender, EventArgs e)
        {
            if (CurrentProject != null)
            {
                try
                {
                    var lddPart = CurrentProject.GenerateLddPart();
                    lddPart.ComputeEdgeOutlines();
                    lddPart.Primitive.Save($"{lddPart.PartID}.xml");
                    foreach (var surface in lddPart.Surfaces)
                        surface.Mesh.Save(surface.GetFileName());
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}