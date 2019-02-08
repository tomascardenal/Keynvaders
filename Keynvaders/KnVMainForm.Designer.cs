namespace Keynvaders
{
    partial class KnVMainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KnVMainForm));
            this.tmrMovement = new System.Windows.Forms.Timer(this.components);
            this.tmrCreator = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.lbDisplay = new System.Windows.Forms.Label();
            this.tmrDisplayScores = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrMovement
            // 
            this.tmrMovement.Interval = 50;
            this.tmrMovement.Tick += new System.EventHandler(this.tmrMovement_Tick);
            // 
            // tmrCreator
            // 
            this.tmrCreator.Interval = 600;
            this.tmrCreator.Tick += new System.EventHandler(this.tmrCreator_Tick);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Metal Gear", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(23, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(156, 62);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START!";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lbDisplay
            // 
            this.lbDisplay.Location = new System.Drawing.Point(41, 19);
            this.lbDisplay.Name = "lbDisplay";
            this.lbDisplay.Size = new System.Drawing.Size(212, 25);
            this.lbDisplay.TabIndex = 2;
            this.lbDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrDisplayScores
            // 
            this.tmrDisplayScores.Tick += new System.EventHandler(this.tmrDisplayScores_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbDisplay);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(218, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 62);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "High Scores";
            // 
            // KnVMainForm
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 101);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "KnVMainForm";
            this.Text = "KeyNvaders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeynvadersMainForm_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keynvaders_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrMovement;
        private System.Windows.Forms.Timer tmrCreator;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lbDisplay;
        private System.Windows.Forms.Timer tmrDisplayScores;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

