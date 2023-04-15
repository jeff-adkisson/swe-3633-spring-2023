namespace WackyChat;

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
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutboundMsg = new System.Windows.Forms.TextBox();
            this.txtInboundMessages = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkShowMyMessages = new System.Windows.Forms.CheckBox();
            this.lblMostRecentMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your Message:";
            // 
            // txtOutboundMsg
            // 
            this.txtOutboundMsg.Location = new System.Drawing.Point(121, 12);
            this.txtOutboundMsg.MaxLength = 200;
            this.txtOutboundMsg.Name = "txtOutboundMsg";
            this.txtOutboundMsg.Size = new System.Drawing.Size(667, 27);
            this.txtOutboundMsg.TabIndex = 1;
            this.txtOutboundMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnOutboundMsgKeyPress);
            // 
            // txtInboundMessages
            // 
            this.txtInboundMessages.AcceptsReturn = true;
            this.txtInboundMessages.Location = new System.Drawing.Point(12, 59);
            this.txtInboundMessages.Multiline = true;
            this.txtInboundMessages.Name = "txtInboundMessages";
            this.txtInboundMessages.ReadOnly = true;
            this.txtInboundMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInboundMessages.Size = new System.Drawing.Size(776, 341);
            this.txtInboundMessages.TabIndex = 2;
            this.txtInboundMessages.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(694, 409);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 29);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.OnClearClick);
            // 
            // chkShowMyMessages
            // 
            this.chkShowMyMessages.AutoSize = true;
            this.chkShowMyMessages.Checked = true;
            this.chkShowMyMessages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowMyMessages.Location = new System.Drawing.Point(511, 412);
            this.chkShowMyMessages.Name = "chkShowMyMessages";
            this.chkShowMyMessages.Size = new System.Drawing.Size(159, 24);
            this.chkShowMyMessages.TabIndex = 3;
            this.chkShowMyMessages.Text = "Show my messages";
            this.chkShowMyMessages.UseVisualStyleBackColor = true;
            this.chkShowMyMessages.CheckedChanged += new System.EventHandler(this.OnShowMyMessagesChanged);
            // 
            // lblMostRecentMsg
            // 
            this.lblMostRecentMsg.AutoSize = true;
            this.lblMostRecentMsg.Location = new System.Drawing.Point(12, 416);
            this.lblMostRecentMsg.Name = "lblMostRecentMsg";
            this.lblMostRecentMsg.Size = new System.Drawing.Size(157, 20);
            this.lblMostRecentMsg.TabIndex = 5;
            this.lblMostRecentMsg.Text = "No messages received";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMostRecentMsg);
            this.Controls.Add(this.chkShowMyMessages);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtInboundMessages);
            this.Controls.Add(this.txtOutboundMsg);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "SWE Wacky Chat!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Label label1;
    private TextBox txtOutboundMsg;
    private TextBox txtInboundMessages;
    private Button btnClear;
    private CheckBox chkShowMyMessages;
    private Label lblMostRecentMsg;
}