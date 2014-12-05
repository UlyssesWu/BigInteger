namespace BigIntCalcDemo
{
    partial class FormCalc
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOp1 = new System.Windows.Forms.TextBox();
            this.txtOp2 = new System.Windows.Forms.TextBox();
            this.btnSub = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnMul = new System.Windows.Forms.Button();
            this.txtAns = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDiv = new System.Windows.Forms.Button();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnTab = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOp1
            // 
            this.txtOp1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOp1.Font = new System.Drawing.Font("微软雅黑", 14.26415F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOp1.Location = new System.Drawing.Point(12, 12);
            this.txtOp1.Name = "txtOp1";
            this.txtOp1.Size = new System.Drawing.Size(560, 35);
            this.txtOp1.TabIndex = 0;
            this.txtOp1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOp2
            // 
            this.txtOp2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOp2.Font = new System.Drawing.Font("微软雅黑", 14.26415F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOp2.Location = new System.Drawing.Point(12, 53);
            this.txtOp2.Name = "txtOp2";
            this.txtOp2.Size = new System.Drawing.Size(560, 35);
            this.txtOp2.TabIndex = 1;
            this.txtOp2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSub
            // 
            this.btnSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSub.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSub.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSub.Font = new System.Drawing.Font("微软雅黑", 16.30189F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSub.Location = new System.Drawing.Point(426, 230);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(70, 65);
            this.btnSub.TabIndex = 2;
            this.btnSub.Text = "-";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 16.30189F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Location = new System.Drawing.Point(426, 159);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 65);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnMul
            // 
            this.btnMul.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMul.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnMul.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnMul.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnMul.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMul.Font = new System.Drawing.Font("微软雅黑", 16.30189F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMul.Location = new System.Drawing.Point(502, 159);
            this.btnMul.Name = "btnMul";
            this.btnMul.Size = new System.Drawing.Size(70, 65);
            this.btnMul.TabIndex = 4;
            this.btnMul.Text = "×";
            this.btnMul.UseVisualStyleBackColor = true;
            this.btnMul.Click += new System.EventHandler(this.btnMul_Click);
            // 
            // txtAns
            // 
            this.txtAns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAns.BackColor = System.Drawing.SystemColors.Info;
            this.txtAns.Font = new System.Drawing.Font("微软雅黑", 14.26415F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAns.Location = new System.Drawing.Point(12, 115);
            this.txtAns.Name = "txtAns";
            this.txtAns.ReadOnly = true;
            this.txtAns.Size = new System.Drawing.Size(560, 35);
            this.txtAns.TabIndex = 5;
            this.txtAns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Answer";
            // 
            // btnDiv
            // 
            this.btnDiv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiv.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDiv.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnDiv.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiv.Font = new System.Drawing.Font("微软雅黑", 16.30189F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDiv.Location = new System.Drawing.Point(502, 230);
            this.btnDiv.Name = "btnDiv";
            this.btnDiv.Size = new System.Drawing.Size(70, 65);
            this.btnDiv.TabIndex = 7;
            this.btnDiv.Text = "÷";
            this.btnDiv.UseVisualStyleBackColor = true;
            this.btnDiv.Click += new System.EventHandler(this.btnDiv_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEqual.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnEqual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnEqual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnEqual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEqual.Font = new System.Drawing.Font("微软雅黑", 16.30189F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEqual.Location = new System.Drawing.Point(350, 159);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(70, 136);
            this.btnEqual.TabIndex = 8;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Visible = false;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopy.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnCopy.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnCopy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("微软雅黑", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCopy.Location = new System.Drawing.Point(12, 159);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(96, 136);
            this.btnCopy.TabIndex = 9;
            this.btnCopy.Text = "Copy Answer";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnTab
            // 
            this.btnTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTab.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTab.Font = new System.Drawing.Font("微软雅黑", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTab.Location = new System.Drawing.Point(114, 159);
            this.btnTab.Name = "btnTab";
            this.btnTab.Size = new System.Drawing.Size(96, 136);
            this.btnTab.TabIndex = 10;
            this.btnTab.Text = "Tab";
            this.btnTab.UseVisualStyleBackColor = true;
            this.btnTab.Click += new System.EventHandler(this.btnTab_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 307);
            this.Controls.Add(this.btnTab);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnEqual);
            this.Controls.Add(this.btnDiv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMul);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.txtOp2);
            this.Controls.Add(this.txtOp1);
            this.Controls.Add(this.txtAns);
            this.MinimumSize = new System.Drawing.Size(466, 343);
            this.Name = "Form1";
            this.Text = "BigIntCalculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOp1;
        private System.Windows.Forms.TextBox txtOp2;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnMul;
        private System.Windows.Forms.TextBox txtAns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDiv;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnTab;
    }
}

