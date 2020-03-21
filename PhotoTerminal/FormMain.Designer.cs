namespace PhotoTerminal
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGlanPaper = new System.Windows.Forms.Button();
            this.buttonMatPaper = new System.Windows.Forms.Button();
            this.buttonCancelOrder = new System.Windows.Forms.Button();
            this.buttonSelAll = new System.Windows.Forms.Button();
            this.buttonDeSelAll = new System.Windows.Forms.Button();
            this.labelPaperType = new System.Windows.Forms.Label();
            this.buttonDoOrder = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.flowLayoutPanelImageSizes = new System.Windows.Forms.FlowLayoutPanel();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // buttonGlanPaper
            // 
            this.buttonGlanPaper.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGlanPaper.Location = new System.Drawing.Point(212, 284);
            this.buttonGlanPaper.Name = "buttonGlanPaper";
            this.buttonGlanPaper.Size = new System.Drawing.Size(200, 200);
            this.buttonGlanPaper.TabIndex = 0;
            this.buttonGlanPaper.Tag = "glan";
            this.buttonGlanPaper.Text = "Глянцевая";
            this.buttonGlanPaper.UseVisualStyleBackColor = true;
            this.buttonGlanPaper.Visible = false;
            this.buttonGlanPaper.Click += new System.EventHandler(this.buttonGlanPaper_Click);
            // 
            // buttonMatPaper
            // 
            this.buttonMatPaper.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonMatPaper.Location = new System.Drawing.Point(612, 284);
            this.buttonMatPaper.Name = "buttonMatPaper";
            this.buttonMatPaper.Size = new System.Drawing.Size(200, 200);
            this.buttonMatPaper.TabIndex = 1;
            this.buttonMatPaper.Tag = "mat";
            this.buttonMatPaper.Text = "Матовая";
            this.buttonMatPaper.UseVisualStyleBackColor = true;
            this.buttonMatPaper.Visible = false;
            this.buttonMatPaper.Click += new System.EventHandler(this.buttonMatPaper_Click);
            // 
            // buttonCancelOrder
            // 
            this.buttonCancelOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancelOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancelOrder.Location = new System.Drawing.Point(771, 620);
            this.buttonCancelOrder.Name = "buttonCancelOrder";
            this.buttonCancelOrder.Size = new System.Drawing.Size(225, 97);
            this.buttonCancelOrder.TabIndex = 2;
            this.buttonCancelOrder.Text = "Отменить заказ";
            this.buttonCancelOrder.UseVisualStyleBackColor = true;
            this.buttonCancelOrder.Visible = false;
            this.buttonCancelOrder.Click += new System.EventHandler(this.buttonCancelOrder_Click);
            // 
            // buttonSelAll
            // 
            this.buttonSelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSelAll.Location = new System.Drawing.Point(78, 620);
            this.buttonSelAll.Name = "buttonSelAll";
            this.buttonSelAll.Size = new System.Drawing.Size(225, 97);
            this.buttonSelAll.TabIndex = 3;
            this.buttonSelAll.Text = "Выбрать все";
            this.buttonSelAll.UseVisualStyleBackColor = true;
            this.buttonSelAll.Visible = false;
            // 
            // buttonDeSelAll
            // 
            this.buttonDeSelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeSelAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDeSelAll.Location = new System.Drawing.Point(309, 620);
            this.buttonDeSelAll.Name = "buttonDeSelAll";
            this.buttonDeSelAll.Size = new System.Drawing.Size(225, 97);
            this.buttonDeSelAll.TabIndex = 4;
            this.buttonDeSelAll.Text = "Снять выделение";
            this.buttonDeSelAll.UseVisualStyleBackColor = true;
            this.buttonDeSelAll.Visible = false;
            // 
            // labelPaperType
            // 
            this.labelPaperType.AutoSize = true;
            this.labelPaperType.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaperType.Location = new System.Drawing.Point(297, 147);
            this.labelPaperType.Name = "labelPaperType";
            this.labelPaperType.Size = new System.Drawing.Size(424, 46);
            this.labelPaperType.TabIndex = 5;
            this.labelPaperType.Text = "Выберите тип бумаги";
            this.labelPaperType.Visible = false;
            // 
            // buttonDoOrder
            // 
            this.buttonDoOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDoOrder.Enabled = false;
            this.buttonDoOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDoOrder.Location = new System.Drawing.Point(540, 620);
            this.buttonDoOrder.Name = "buttonDoOrder";
            this.buttonDoOrder.Size = new System.Drawing.Size(225, 97);
            this.buttonDoOrder.TabIndex = 6;
            this.buttonDoOrder.Text = "Оформить заказ";
            this.buttonDoOrder.UseVisualStyleBackColor = true;
            this.buttonDoOrder.Visible = false;
            this.buttonDoOrder.Click += new System.EventHandler(this.buttonDoOrder_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBack.Location = new System.Drawing.Point(12, 620);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(60, 97);
            this.buttonBack.TabIndex = 7;
            this.buttonBack.Text = "Н\nА\nЗ\nА\nД";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Visible = false;
            // 
            // flowLayoutPanelImageSizes
            // 
            this.flowLayoutPanelImageSizes.AutoScroll = true;
            this.flowLayoutPanelImageSizes.Location = new System.Drawing.Point(287, 12);
            this.flowLayoutPanelImageSizes.Name = "flowLayoutPanelImageSizes";
            this.flowLayoutPanelImageSizes.Size = new System.Drawing.Size(450, 705);
            this.flowLayoutPanelImageSizes.TabIndex = 8;
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Location = new System.Drawing.Point(0, 720);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(1009, 20);
            this.progressBarUpload.TabIndex = 10;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.progressBarUpload);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonDoOrder);
            this.Controls.Add(this.labelPaperType);
            this.Controls.Add(this.buttonDeSelAll);
            this.Controls.Add(this.buttonSelAll);
            this.Controls.Add(this.buttonCancelOrder);
            this.Controls.Add(this.buttonMatPaper);
            this.Controls.Add(this.buttonGlanPaper);
            this.Controls.Add(this.flowLayoutPanelImageSizes);
            this.Name = "FormMain";
            this.Text = "PhotoTerminal";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGlanPaper;
        private System.Windows.Forms.Button buttonMatPaper;
        private System.Windows.Forms.Label labelPaperType;
        public System.Windows.Forms.Button buttonSelAll;
        public System.Windows.Forms.Button buttonDeSelAll;
        public System.Windows.Forms.Button buttonDoOrder;
        public System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelImageSizes;
        public System.Windows.Forms.Button buttonCancelOrder;
        private System.Windows.Forms.ProgressBar progressBarUpload;
    }
}

