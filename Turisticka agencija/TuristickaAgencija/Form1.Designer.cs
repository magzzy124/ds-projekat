namespace TuristickaAgencija;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.klijentiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.paketiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.rezervacijeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.statusStrip1 = new System.Windows.Forms.StatusStrip();
        this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPageClients = new System.Windows.Forms.TabPage();
        this.tabPagePackages = new System.Windows.Forms.TabPage();
        this.tabPageReservations = new System.Windows.Forms.TabPage();
        this.menuStrip1.SuspendLayout();
        this.statusStrip1.SuspendLayout();
        this.tabControl1.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.klijentiToolStripMenuItem,
            this.paketiToolStripMenuItem,
            this.rezervacijeToolStripMenuItem});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(1200, 24);
        this.menuStrip1.TabIndex = 0;
        this.menuStrip1.Text = "menuStrip1";
        
        // 
        // klijentiToolStripMenuItem
        // 
        this.klijentiToolStripMenuItem.Name = "klijentiToolStripMenuItem";
        this.klijentiToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
        this.klijentiToolStripMenuItem.Text = "Klijenti";
        
        // 
        // paketiToolStripMenuItem
        // 
        this.paketiToolStripMenuItem.Name = "paketiToolStripMenuItem";
        this.paketiToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
        this.paketiToolStripMenuItem.Text = "Paketi";
        
        // 
        // rezervacijeToolStripMenuItem
        // 
        this.rezervacijeToolStripMenuItem.Name = "rezervacijeToolStripMenuItem";
        this.rezervacijeToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
        this.rezervacijeToolStripMenuItem.Text = "Rezervacije";
        
        // 
        // statusStrip1
        // 
        this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
        this.statusStrip1.Location = new System.Drawing.Point(0, 675);
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
        this.statusStrip1.TabIndex = 1;
        this.statusStrip1.Text = "statusStrip1";
        
        // 
        // toolStripStatusLabel1
        // 
        this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
        this.toolStripStatusLabel1.Text = "Spreman";
        
        // 
        // tabControl1
        // 
        this.tabControl1.Controls.Add(this.tabPageClients);
        this.tabControl1.Controls.Add(this.tabPagePackages);
        this.tabControl1.Controls.Add(this.tabPageReservations);
        this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabControl1.Location = new System.Drawing.Point(0, 24);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(1200, 651);
        this.tabControl1.TabIndex = 2;
        
        // 
        // tabPageClients
        // 
        this.tabPageClients.Location = new System.Drawing.Point(4, 22);
        this.tabPageClients.Name = "tabPageClients";
        this.tabPageClients.Padding = new System.Windows.Forms.Padding(3);
        this.tabPageClients.Size = new System.Drawing.Size(1192, 625);
        this.tabPageClients.TabIndex = 0;
        this.tabPageClients.Text = "Klijenti";
        this.tabPageClients.UseVisualStyleBackColor = true;
        
        // 
        // tabPagePackages
        // 
        this.tabPagePackages.Location = new System.Drawing.Point(4, 22);
        this.tabPagePackages.Name = "tabPagePackages";
        this.tabPagePackages.Padding = new System.Windows.Forms.Padding(3);
        this.tabPagePackages.Size = new System.Drawing.Size(1192, 625);
        this.tabPagePackages.TabIndex = 1;
        this.tabPagePackages.Text = "Paketi";
        this.tabPagePackages.UseVisualStyleBackColor = true;
        
        // 
        // tabPageReservations
        // 
        this.tabPageReservations.Location = new System.Drawing.Point(4, 22);
        this.tabPageReservations.Name = "tabPageReservations";
        this.tabPageReservations.Size = new System.Drawing.Size(1192, 625);
        this.tabPageReservations.TabIndex = 2;
        this.tabPageReservations.Text = "Rezervacije";
        this.tabPageReservations.UseVisualStyleBackColor = true;
        
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1200, 697);
        this.Controls.Add(this.tabControl1);
        this.Controls.Add(this.statusStrip1);
        this.Controls.Add(this.menuStrip1);
        this.IsMdiContainer = true;
        this.MainMenuStrip = this.menuStrip1;
        this.Name = "MainForm";
        this.Text = "Turistička Agencija";
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.statusStrip1.ResumeLayout(false);
        this.statusStrip1.PerformLayout();
        this.tabControl1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #region Windows Form Designer generated controls
    
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem klijentiToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem paketiToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rezervacijeToolStripMenuItem;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPageClients;
    private System.Windows.Forms.TabPage tabPagePackages;
    private System.Windows.Forms.TabPage tabPageReservations;
    
    #endregion

    #endregion
}
