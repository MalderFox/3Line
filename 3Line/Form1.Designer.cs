namespace _3Line {
    partial class GameOfGodsForm {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.fieldPanel = new System.Windows.Forms.Panel();
            this.search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fieldPanel
            // 
            this.fieldPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fieldPanel.Location = new System.Drawing.Point(2, 2);
            this.fieldPanel.Name = "fieldPanel";
            this.fieldPanel.Size = new System.Drawing.Size(451, 351);
            this.fieldPanel.TabIndex = 0;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(12, 369);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(75, 23);
            this.search.TabIndex = 1;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.screenshotClick);
            // 
            // GameOfGodsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 398);
            this.Controls.Add(this.search);
            this.Controls.Add(this.fieldPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameOfGodsForm";
            this.Text = "Game Of Gods";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel fieldPanel;
        private System.Windows.Forms.Button search;
    }
}

