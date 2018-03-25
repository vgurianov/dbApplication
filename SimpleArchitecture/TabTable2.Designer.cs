namespace UserControls
{
    partial class TabContentForOperation
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
            this.OperationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OperationButton
            // 
            this.OperationButton.Location = new System.Drawing.Point(407, 72);
            this.OperationButton.Name = "OperationButton";
            this.OperationButton.Size = new System.Drawing.Size(75, 23);
            this.OperationButton.TabIndex = 7;
            this.OperationButton.Text = "Operation";
            this.OperationButton.UseVisualStyleBackColor = true;
            this.OperationButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // TabContentForOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.OperationButton);
            this.Name = "TabContentForOperation";
            this.Controls.SetChildIndex(this.OperationButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OperationButton;
    }
}
