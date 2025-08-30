namespace TuristickaAgencija.Forms
{
    partial class AddClientForm
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
            this.labelIme = new System.Windows.Forms.Label();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.labelPrezime = new System.Windows.Forms.Label();
            this.txtPrezime = new System.Windows.Forms.TextBox();
            this.labelBrojPasosa = new System.Windows.Forms.Label();
            this.txtBrojPasosa = new System.Windows.Forms.TextBox();
            this.labelDatumRodjenja = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.labelEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.labelBrojTelefona = new System.Windows.Forms.Label();
            this.txtBrojTelefona = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // 
            // labelIme
            // 
            this.labelIme.AutoSize = true;
            this.labelIme.Location = new System.Drawing.Point(30, 30);
            this.labelIme.Name = "labelIme";
            this.labelIme.Size = new System.Drawing.Size(27, 13);
            this.labelIme.TabIndex = 0;
            this.labelIme.Text = "Ime:";
            
            // 
            // txtIme
            // 
            this.txtIme.Location = new System.Drawing.Point(140, 27);
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(200, 20);
            this.txtIme.TabIndex = 1;
            
            // 
            // labelPrezime
            // 
            this.labelPrezime.AutoSize = true;
            this.labelPrezime.Location = new System.Drawing.Point(30, 60);
            this.labelPrezime.Name = "labelPrezime";
            this.labelPrezime.Size = new System.Drawing.Size(47, 13);
            this.labelPrezime.TabIndex = 2;
            this.labelPrezime.Text = "Prezime:";
            
            // 
            // txtPrezime
            // 
            this.txtPrezime.Location = new System.Drawing.Point(140, 57);
            this.txtPrezime.Name = "txtPrezime";
            this.txtPrezime.Size = new System.Drawing.Size(200, 20);
            this.txtPrezime.TabIndex = 3;
            
            // 
            // labelBrojPasosa
            // 
            this.labelBrojPasosa.AutoSize = true;
            this.labelBrojPasosa.Location = new System.Drawing.Point(30, 90);
            this.labelBrojPasosa.Name = "labelBrojPasosa";
            this.labelBrojPasosa.Size = new System.Drawing.Size(67, 13);
            this.labelBrojPasosa.TabIndex = 4;
            this.labelBrojPasosa.Text = "Broj pasoša:";
            
            // 
            // txtBrojPasosa
            // 
            this.txtBrojPasosa.Location = new System.Drawing.Point(140, 87);
            this.txtBrojPasosa.Name = "txtBrojPasosa";
            this.txtBrojPasosa.Size = new System.Drawing.Size(200, 20);
            this.txtBrojPasosa.TabIndex = 5;
            this.txtBrojPasosa.Leave += new System.EventHandler(this.txtBrojPasosa_Leave);
            
            // 
            // labelDatumRodjenja
            // 
            this.labelDatumRodjenja.AutoSize = true;
            this.labelDatumRodjenja.Location = new System.Drawing.Point(30, 120);
            this.labelDatumRodjenja.Name = "labelDatumRodjenja";
            this.labelDatumRodjenja.Size = new System.Drawing.Size(80, 13);
            this.labelDatumRodjenja.TabIndex = 6;
            this.labelDatumRodjenja.Text = "Datum rođenja:";
            
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(140, 117);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker.TabIndex = 7;
            this.dateTimePicker.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(30, 150);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(35, 13);
            this.labelEmail.TabIndex = 8;
            this.labelEmail.Text = "Email:";
            
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(140, 147);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 9;
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            
            // 
            // labelBrojTelefona
            // 
            this.labelBrojTelefona.AutoSize = true;
            this.labelBrojTelefona.Location = new System.Drawing.Point(30, 180);
            this.labelBrojTelefona.Name = "labelBrojTelefona";
            this.labelBrojTelefona.Size = new System.Drawing.Size(73, 13);
            this.labelBrojTelefona.TabIndex = 10;
            this.labelBrojTelefona.Text = "Broj telefona:";
            
            // 
            // txtBrojTelefona
            // 
            this.txtBrojTelefona.Location = new System.Drawing.Point(140, 177);
            this.txtBrojTelefona.Name = "txtBrojTelefona";
            this.txtBrojTelefona.Size = new System.Drawing.Size(200, 20);
            this.txtBrojTelefona.TabIndex = 11;
            
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(140, 230);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Sačuvaj";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(250, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Otkaži";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // 
            // AddClientForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 291);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtBrojTelefona);
            this.Controls.Add(this.labelBrojTelefona);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.labelDatumRodjenja);
            this.Controls.Add(this.txtBrojPasosa);
            this.Controls.Add(this.labelBrojPasosa);
            this.Controls.Add(this.txtPrezime);
            this.Controls.Add(this.labelPrezime);
            this.Controls.Add(this.txtIme);
            this.Controls.Add(this.labelIme);
            this.Name = "AddClientForm";
            this.Text = "Dodaj klijenta";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelIme;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label labelPrezime;
        private System.Windows.Forms.TextBox txtPrezime;
        private System.Windows.Forms.Label labelBrojPasosa;
        private System.Windows.Forms.TextBox txtBrojPasosa;
        private System.Windows.Forms.Label labelDatumRodjenja;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label labelBrojTelefona;
        private System.Windows.Forms.TextBox txtBrojTelefona;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
