﻿using NavalRegimentManager.dao;
using NavalRegimentManager.Model;
using NavalRegimentManager.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NavalRegimentManager
{
    public partial class MainForm : Form
    {
        private int rowCount;
        private int columnCount;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.rowCount = this.tableView.RowCount;
            this.columnCount = this.tableView.ColumnCount;
            showThisWeekRecord();
        }

        private void showThisWeekRecord()
        {
            DataInfoDao dataInfoDao = new DataInfoDao();
            List<DataInfo> dataInfoList = dataInfoDao.getThisWeekRecord();
            bindData(dataInfoList);
        }

        private void showNear4Record()
        {
            DataInfoDao dataInfoDao = new DataInfoDao();
            RecordDao recordDao = new RecordDao();
            int maxIndex = recordDao.getMaxIndex();
            int startIndex = maxIndex - 4 < 0 ? 0 : maxIndex - 4;
            List<DataInfo> dataInfoList = dataInfoDao.getRecordBetweenIndex(startIndex, maxIndex);
            bindData(dataInfoList);
        }

        private void showAllRecord()
        {
            DataInfoDao dataInfoDao = new DataInfoDao();
            List<DataInfo> dataInfoList = dataInfoDao.getAllRecord();
            bindData(dataInfoList);
        }


        private void bindData(List<DataInfo> dataInfoList)
        {
            int index = 1;
            DateTime weekStart = DateTimeHelper.getWeekStart();
            DateTime weekEnd = DateTimeHelper.getWeekEnd();
            this.tableView.Controls.Clear();
            for (int i = 0; i < dataInfoList.Count; i++)
            {
                int rowNo = (index - 1) % 30;
                int columnNo = (index - 1) / 30;
                bool isNewPeople = dataInfoList[i].CreateDate >= weekStart && dataInfoList[i].CreateDate <= weekEnd;
                TableLayoutPanel tablePanel = new TableLayoutPanel();
                tablePanel.Dock = DockStyle.Fill;
                tablePanel.Margin = new Padding(0);
                tablePanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38));
                tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38));
                tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8));
                tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16));
                Label label1 = new Label();
                label1.Text = dataInfoList[i].Uid.ToString();
                label1.Dock = DockStyle.Fill;
                label1.Font= new System.Drawing.Font("微软雅黑", 10F);
                tablePanel.Controls.Add(label1, 0, 0);
                Label label2 = new Label();
                label2.Text = dataInfoList[i].MemberName.ToString();
                label2.Dock = DockStyle.Fill;
                label2.Font = new System.Drawing.Font("微软雅黑", 10F);
                tablePanel.Controls.Add(label2, 1, 0);
                Label label3 = new Label();
                label3.Text = dataInfoList[i].Count.ToString();
                label3.Dock = DockStyle.Fill;
                label3.Font = new System.Drawing.Font("微软雅黑", 10F);
                label3.AutoSize = true;
                tablePanel.Controls.Add(label3, 2, 0);
                Button button = new Button();
                button.Text = "复制";
                button.Dock = DockStyle.Fill;
                button.Margin = new Padding(0);
                button.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                button.Click += new System.EventHandler(this.btnCopyId_Click);
                button.Tag = dataInfoList[i].Uid;
                tablePanel.Controls.Add(button, 3, 0);
                if (isNewPeople)
                {
                    tablePanel.Font= new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    tablePanel.ForeColor = System.Drawing.Color.LightGreen;
                }
                this.tableView.Controls.Add(tablePanel, columnNo, rowNo);
                index++;
            }
        }

        public void AddMembers(string memberInfoStr)
        {
            MemberDao memberDao = new MemberDao();
            try
            {
                memberDao.Db.BeginTran();
                if (memberInfoStr == null || memberInfoStr.Trim().Length == 0) return;
                memberInfoStr = memberInfoStr.Trim();
                memberInfoStr = memberInfoStr.Replace("：", ":");
                memberInfoStr = memberInfoStr.Replace("，", ",");
                memberInfoStr = memberInfoStr.Replace("\r\n", "");
                string[] memberInfo = memberInfoStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in memberInfo)
                {
                    string[] attribute = item.Split(':');
                    Member member = new Member(attribute[0], attribute[1]);
                    memberDao.saveOrUpdate(member);
                }
                memberDao.Db.CommitTran();
                showThisWeekRecord();
            }
            catch (Exception ex)
            {
                memberDao.Db.RollbackTran();
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DelMembers(string memberInfoStr)
        {
            MemberDao memberDao = new MemberDao();
            try
            {
                memberDao.Db.BeginTran();
                if (memberInfoStr == null || memberInfoStr.Trim().Length == 0) return;
                memberInfoStr = memberInfoStr.Trim();
                memberInfoStr = memberInfoStr.Replace("，", ",");
                memberInfoStr = memberInfoStr.Replace("\r\n", "");
                string[] recordInfo = memberInfoStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in recordInfo) memberDao.delMemberById(item);
                memberDao.Db.CommitTran();
                showThisWeekRecord();
            }
            catch (Exception ex)
            {
                memberDao.Db.RollbackTran();
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AddRecords(string recordInfoStr)
        {
            RecordDao recordDao = new RecordDao();
            try
            {
                recordDao.Db.BeginTran();
                int maxIndex = recordDao.getMaxIndex();
                if (recordInfoStr == null || recordInfoStr.Trim().Length == 0) return;
                recordInfoStr = recordInfoStr.Trim();
                recordInfoStr = recordInfoStr.Replace("，", ",");
                recordInfoStr = recordInfoStr.Replace("\r\n", "");
                string[] recordInfo = recordInfoStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in recordInfo) recordDao.Insert(new Record(item, maxIndex + 1));
                recordDao.Db.CommitTran();
                showThisWeekRecord();
            }
            catch (Exception ex)
            {
                recordDao.Db.RollbackTran();
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAddMember_Click(object sender, EventArgs e)
        {
            AddMemberForm addMemberForm = new AddMemberForm(this);
            addMemberForm.Show();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            AddRecordForm addRecordForm = new AddRecordForm(this);
            addRecordForm.Show();
        }

        private void btnDelMember_Click(object sender, EventArgs e)
        {
            DelMemberForm delMemberForm = new DelMemberForm(this);
            delMemberForm.Show();
        }

        private void btnCopyId_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Clipboard.SetDataObject(button.Tag);
        }

        private void btnNear4Record_Click(object sender, EventArgs e)
        {
            showNear4Record();
        }

        private void btnAllRecord_Click(object sender, EventArgs e)
        {
            showAllRecord();
        }
    }
}
