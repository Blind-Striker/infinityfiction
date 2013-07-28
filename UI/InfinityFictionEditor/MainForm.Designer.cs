using InfinityFiction.UI.InfinityFictionEditor.Core.WinFormControls;

namespace InfinityFiction.UI.InfinityFictionEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataContext = new System.Windows.Forms.BindingSource(this.components);
            this.treeResources = new InfinityFiction.UI.InfinityFictionEditor.Core.WinFormControls.DataTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.dataContext)).BeginInit();
            this.SuspendLayout();
            // 
            // dataContext
            // 
            this.dataContext.DataSource = typeof(InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels.MainViewModel);
            // 
            // treeResources
            // 
            this.treeResources.DataMember = "TreeViewItems";
            this.treeResources.DataSource = this.dataContext;
            this.treeResources.FullRowSelect = true;
            this.treeResources.HideSelection = false;
            this.treeResources.HotTracking = true;
            this.treeResources.IDColumn = "Id";
            this.treeResources.Location = new System.Drawing.Point(12, 12);
            this.treeResources.Name = "treeResources";
            this.treeResources.NameColumn = "Name";
            this.treeResources.ParentIDColumn = "ParentId";
            this.treeResources.Size = new System.Drawing.Size(229, 573);
            this.treeResources.TabIndex = 0;
            this.treeResources.ValueColumn = "Id";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 597);
            this.Controls.Add(this.treeResources);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataContext)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource dataContext;
        private DataTreeView treeResources;
    }
}