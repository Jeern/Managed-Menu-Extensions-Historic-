namespace ManagedMenuAddInManagerWinForm
{
    partial class AddInManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddInManager));
            this.AddInList = new System.Windows.Forms.ListView();
            this.AddIns = new System.Windows.Forms.ColumnHeader();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddInList
            // 
            this.AddInList.CheckBoxes = true;
            this.AddInList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AddIns});
            this.AddInList.Location = new System.Drawing.Point(13, 13);
            this.AddInList.Name = "AddInList";
            this.AddInList.Size = new System.Drawing.Size(267, 165);
            this.AddInList.TabIndex = 0;
            this.AddInList.UseCompatibleStateImageBehavior = false;
            this.AddInList.View = System.Windows.Forms.View.Details;
            this.AddInList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.AddInList_ItemChecked);
            // 
            // AddIns
            // 
            this.AddIns.Text = "AddIns - check To Enable";
            this.AddIns.Width = 200;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(124, 184);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "&Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitButton.Location = new System.Drawing.Point(205, 184);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.Text = "&Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // AddInManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ExitButton;
            this.ClientSize = new System.Drawing.Size(292, 217);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.AddInList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddInManager";
            this.Text = "AddIn Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView AddInList;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ColumnHeader AddIns;
    }
}

