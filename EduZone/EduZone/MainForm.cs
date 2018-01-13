using BusinessModel;
using DomainModel.BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduZone
{
    public partial class MainForm : Form
    {
        DomainModel.BusinessObject.User user;
        LoginManager loginManager = new LoginManager();
        StudentManager studentManager = new StudentManager();
        GameCodingManager gameCodingManager = new GameCodingManager();
        MoneyListManager moneyListManager = new MoneyListManager();
        List<AgeRange> ageRanges = new List<AgeRange>();
        List<Group> runningGroups = new List<Group>();
        List<Student> students;
        public MainForm()
        {
            InitializeComponent();
            lst_PanelController.SelectedIndex = 0;
            gb_groupSelection.Visible = false;
            gb_SystemOptions.Visible = false;
            gb_ManualSelection.Visible = false;
            gb_AddKids.Visible = false;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            user = LoginForm.user;
            loginManager.LogoutUser(user);
            switch (MessageBox.Show(this, "Are you sure you want to log out?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    {
                        user = LoginForm.user;
                        loginManager.LogoutUser(user);
                        System.Environment.Exit(1);
                        break;
                    }
                default:
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'eduZoneDBDataSet2.MoneyList' table. You can move, or remove it, as needed.
            this.moneyListTableAdapter.Fill(this.eduZoneDBDataSet2.MoneyList);
            // TODO: This line of code loads data into the 'eduZoneDBDataSet1.STEM' table. You can move, or remove it, as needed.
            this.sTEMTableAdapter.Fill(this.eduZoneDBDataSet1.STEM);
            // TODO: This line of code loads data into the 'eduZoneDBDataSet.LoginHistory' table. You can move, or remove it, as needed.
            this.loginHistoryTableAdapter.Fill(this.eduZoneDBDataSet.LoginHistory);
            // TODO: This line of code loads data into the 'eduZoneDBDataSet.LoginHistory' table. You can move, or remove it, as needed.
            this.loginHistoryTableAdapter.Fill(this.eduZoneDBDataSet.LoginHistory);

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.loginHistoryTableAdapter.FillBy(this.eduZoneDBDataSet.LoginHistory);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to log out?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    {
                        user = LoginForm.user;
                        loginManager.LogoutUser(user);
                        System.Environment.Exit(1);
                        break;
                    }
                default:
                    break;
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            String selectedText = lst_PanelController.SelectedItem.ToString();
            if (selectedText.Contains("Add Kids"))
            {
                gb_AddKids.Visible = true;
                gb_groupSelection.Visible = false;
                gb_SystemOptions.Visible = false;
                gb_ManualSelection.Visible = false;
                ageRanges=studentManager.LoadAgeRanges();
                lb_ChooseError.Visible = false;
            }
            else if (selectedText.Contains("Group & Selection"))
            {
                gb_groupSelection.Visible = true;
                gb_AddKids.Visible = false;
                gb_SystemOptions.Visible = false;
                gb_ManualSelection.Visible = false;
                cb_ageGroup.Items.Clear();
                foreach (Group runGroup in runningGroups)
                {
                    cb_ageGroup.Items.Add(runGroup.ageRange.ID);
                }
            }
            else if (selectedText.Contains("System Options"))
            {
                gb_SystemOptions.Visible = true;
                gb_AddKids.Visible = false;
                gb_groupSelection.Visible = false;
                gb_ManualSelection.Visible = false;
                if (cb_ageGroup.SelectedItem != null)
                {
                    tb_GroupName.Text = cb_ageGroup.SelectedItem.ToString();
                 }
                List<Student> students = GetStudentsIngroup(tb_GroupName.Text);
                List<int> excludedModulesIds= GetTakenModulesIdAllStudent(students);
                clearallLists();
                FillScienceList(excludedModulesIds);
                FillEngineeringList(excludedModulesIds);
                FillMathList(excludedModulesIds);
                FillTechnology(excludedModulesIds);
            }
            else if (selectedText.Contains("Manual Selection"))
            {
                gb_ManualSelection.Visible = true;
                gb_SystemOptions.Visible = false;
                gb_AddKids.Visible = false;
                gb_groupSelection.Visible = false;
                if (cb_ageGroup.SelectedItem != null)
                {
                    tb_GroupMan.Text = cb_ageGroup.SelectedItem.ToString();
                }
            }
        }

        private void clearallLists()
        {
            if (lst_Science.Items.Count != 0)
            {
                lst_Science.DataSource = null;
                lst_Science.Items.Clear();
            }
            if (lst_Technology.Items.Count != 0)
            {
                lst_Technology.DataSource = null;
                lst_Technology.Items.Clear();
            }
            if (lst_Engineering.Items.Count != 0)
            {
                lst_Engineering.DataSource = null;
                lst_Engineering.Items.Clear();
            }
            if (lst_Math.Items.Count != 0)
            {
                lst_Math.DataSource = null;
                lst_Math.Items.Clear();
            }
        }

        private List<int> GetTakenModulesIdAllStudent(List<Student> students)
        {
            List<int> allModules = new List<int>();
            foreach (Student student in students)
            {
                List<int> takenModules = gameCodingManager.GetModulesIdTaken(student);
                allModules.AddRange(takenModules);
            }
            return allModules;
        }
        public void FillScienceList(List<int> excludedModulesIds)
        {
            List<DomainModel.BusinessObject.GameCoding> gameCodingList = new List<GameCoding>();
            STEM stemDomain = new STEM();
            stemDomain.Id = "S";
            if (cb_ageGroup.SelectedItem != null)
            {
                AgeRange ageRange = GetAgeRange(cb_ageGroup.SelectedItem.ToString());
                gameCodingList = gameCodingManager.LoadGameModuleByAgeAndSTEM(ageRange, stemDomain);
                List<SelectValue> gameNamesList = GetGameName(gameCodingList, excludedModulesIds);
                lst_Science.DataSource = gameNamesList;
                lst_Science.DisplayMember = "Text";
            }
            lst_Science.SelectedIndex = -1;
        }
        public void FillEngineeringList(List<int> excludedModulesIds)
        {
            List<DomainModel.BusinessObject.GameCoding> gameCodingList = new List<GameCoding>();
            STEM stemDomain = new STEM();
            stemDomain.Id = "E";
            if (cb_ageGroup.SelectedItem != null)
            {
                AgeRange ageRange = GetAgeRange(cb_ageGroup.SelectedItem.ToString());
                gameCodingList = gameCodingManager.LoadGameModuleByAgeAndSTEM(ageRange, stemDomain);
                List<SelectValue> gameNamesList = GetGameName(gameCodingList, excludedModulesIds);
                lst_Engineering.DataSource = gameNamesList;
                lst_Engineering.DisplayMember = "Text";
            }
            lst_Engineering.SelectedIndex = -1;
        }
        public void FillMathList(List<int> excludedModulesIds)
        {
            List<DomainModel.BusinessObject.GameCoding> gameCodingList = new List<GameCoding>();
            STEM stemDomain = new STEM();
            stemDomain.Id = "M";
            if (cb_ageGroup.SelectedItem != null)
            {
                AgeRange ageRange = GetAgeRange(cb_ageGroup.SelectedItem.ToString());
                gameCodingList = gameCodingManager.LoadGameModuleByAgeAndSTEM(ageRange, stemDomain);
                List<SelectValue> gameNamesList = GetGameName(gameCodingList, excludedModulesIds);
                lst_Math.DataSource = gameNamesList;
                lst_Math.DisplayMember = "Text";
            }
            lst_Math.SelectedIndex = -1;
        }
        public void FillTechnology(List<int> excludedModulesIds)
        {
            List<DomainModel.BusinessObject.GameCoding> gameCodingList = new List<GameCoding>();
            STEM stemDomain = new STEM();
            stemDomain.Id = "T";
            if (cb_ageGroup.SelectedItem != null)
            {
                AgeRange ageRange = GetAgeRange(cb_ageGroup.SelectedItem.ToString());
                gameCodingList = gameCodingManager.LoadGameModuleByAgeAndSTEM(ageRange, stemDomain);
                List<SelectValue> gameNamesList = GetGameName(gameCodingList, excludedModulesIds);
                lst_Technology.DataSource = gameNamesList;
                lst_Technology.DisplayMember = "Text";
            }
            lst_Technology.SelectedIndex = -1;
        }
        private List<SelectValue> GetGameName(List<DomainModel.BusinessObject.GameCoding> gameCodingList, List<int> excludedModulesIds)
        {
            List<int> gameIds = GetGamesInUse(dtp_SessionTime.Value);
            List<SelectValue> codes = new List<SelectValue>();
            foreach (DomainModel.BusinessObject.GameCoding gameCode in gameCodingList)
            {
                if (!gameIds.Contains(gameCode.Id))
                { 
                String code = gameCode.Stem.Id + gameCode.Branch.branchNumPerStem + gameCode.AgeRange.ID + gameCode.Game.Id;
                foreach (DomainModel.BusinessObject.Module gameModule in gameCode.Modules)
                {
                        if (!excludedModulesIds.Contains(gameModule.Id))
                        {
                            String codeModule = code + "(" + gameModule.ModuleId + ")";
                            SelectValue selected = new SelectValue();
                            selected.Text = gameCode.Game.GameName + "(" + gameModule.ModuleId + ")";
                            selected.Value = codeModule;
                            codes.Add(selected);
                    }
                    }
                }
            }
            return codes;
        }
        private List<int> GetGamesInUse(DateTime sessionDateTime)
        {
            return gameCodingManager.GetGameCodeInUse(sessionDateTime);
        }
        private AgeRange GetAgeRange(String ageID)
        {
            AgeRange ageRange = new AgeRange();
            foreach (AgeRange ageR in ageRanges)
            {
                if (ageR.ID == ageID)
                    ageRange = ageR;
            }
            return ageRange;
        }
        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text.Contains("Add New Student"))
            {
                gb_ManualSelection.Visible = false;
                gb_SystemOptions.Visible = false;
                gb_AddKids.Visible = false;
                gb_groupSelection.Visible = false;
            }
            else if ((tabControl1.SelectedTab.Text.Contains("Admin Panel")))
            {
                gb_ManualSelection.Visible = false;
                gb_SystemOptions.Visible = false;
                gb_AddKids.Visible = false;
                gb_groupSelection.Visible = false;
                tb_cashPerDay.Text = loginManager.GetTotalCashPerDay()+"";
            }
            else if (tabControl1.SelectedTab.Text.Contains("Start Sessions"))
            {
                lst_PanelController.SelectedIndex = 0;
                gb_AddKids.Visible = true;
                gb_ManualSelection.Visible = false;
                gb_SystemOptions.Visible = false;
                gb_groupSelection.Visible = false;
            }
        }
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            DomainModel.BusinessObject.Student student = new DomainModel.BusinessObject.Student();
            student.StudentName = tbStudentName.Text;
            student.ParentName = tbParentName.Text;
            student.BirthDate = dtpBirthDate.Value.Date;
            student.MobileNumber = tb_StudentMobile.Text;
            student.ParentFingurePrint = tbFingrePrint.Text;
            studentManager.AddStudent(student);
            tbStudentName.Text = "";
            tbParentName.Text = "";
            tb_StudentMobile.Text="";
            tb_Age.Text = "";
            tbAge.Text = "";
            tbFingrePrint.Text = "";
        }
        private void dtpBirthDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime birthDate = dtpBirthDate.Value.Date;
            int age = GetAge(birthDate);
            tbAge.Text = age+"";
        }
        private int GetAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthDate.Year;
            if (birthDate > currentDate.AddYears(-age)) age--;
            return age;
        }

        private void lst_KidsNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_KidsNames.Items.Count != 0)
            {
                if (lst_KidsNames.SelectedItem != null)
                {
                    String studentName = lst_KidsNames.SelectedItem.ToString();
                    Student student = GetSelectedStudent(studentName);
                    tb_Age.Text = GetAge(student.BirthDate) + "";
                    tb_ParentNameShow.Text = student.ParentName;
                }
            }
        }

        private Student GetSelectedStudent(String studentName)
        {
            Student selectedStudent=new Student();
            foreach (Student student in students)
            {
                if (student.StudentName == studentName)
                {
                    selectedStudent = student;
                }
            }
            return selectedStudent;
        }

        private void btn_SearchMobile_Click(object sender, EventArgs e)
        {
            lb_ErrorSearch.Visible = false;
            lst_KidsNames.Items.Clear();
            students = new List<Student>();
            students = studentManager.GetStudentsByMobileNumber(tb_MobileSearch.Text);
            if ((tb_MobileSearch.Text == ""))
            {
                lb_ErrorSearch.Text = "Please Enter mobile number";
                lb_ErrorSearch.Visible = true;
                tb_MobileSearch.Text = "";
            }
            else if (students.Count == 0)
            {
                lb_ErrorSearch.Text = "Child not registered";
                lb_ErrorSearch.Visible = true;
                tb_MobileSearch.Text = "";
            }
            else
            {
                foreach (Student student in students)
                {
                    lst_KidsNames.Items.Add(student.StudentName);
                }
            }
        }

        private void btn_AddToGroup_Click(object sender, EventArgs e)
        {
            if (lst_KidsNames.SelectedItem != null)
            {
                String studentName = lst_KidsNames.SelectedItem.ToString();
                Student selectedStudent = GetSelectedStudent(studentName);
                AddStudentToGroup(selectedStudent);
            }
            else
            {
                lb_ErrorSearch.Text = "Please Select Kid name from list";
                lb_ErrorSearch.Visible = true;
            }
        }
        private void AddStudentToGroup(Student student)
        {
            bool added = false;
            int age = GetAge(student.BirthDate);
            AgeRange ageRange=GetStudentAgeRange(age);
            if (runningGroups.Count == 0)
            {
                CreateGroup(ageRange,student);
            }
            else
            {
                foreach (Group runGroup in runningGroups)
                {
                    if (runGroup.ageRange.ID == ageRange.ID)
                    {
                        if (!runGroup.students.Contains(student))
                        {
                            runGroup.students.Add(student);
                        }
                        added = true;
                    }
                }
                if (added == false)
                {
                    CreateGroup(ageRange,student);
                }
            }
        }
        private void CreateGroup(AgeRange ageRange,Student student)
        {
            Group group = new Group();
            group.ageRange = ageRange;
            List<Student> students = new List<Student>();
            students.Add(student);
            group.students = students;
            runningGroups.Add(group);

        }
        private AgeRange GetStudentAgeRange(int age)
        {
            AgeRange selectedRange = new AgeRange();
            foreach (AgeRange ageRange in ageRanges)
            {
                if ((age >= ageRange.minAge) && (age <= ageRange.maxAge))
                {
                    selectedRange = ageRange;
                }
            }
            return selectedRange;
        }

        private void cb_ageGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_groupName.Items.Clear();
            String selectedGroupText=cb_ageGroup.SelectedItem.ToString();
            foreach (Group runGroup in runningGroups)
            {
                if (runGroup.ageRange.ID.Trim().Equals(selectedGroupText.Trim()))
                {
                    foreach(Student student in runGroup.students)
                    {
                        lst_groupName.Items.Add(student.StudentName);
                    }
                }
            }
        }

        private void btn_ChooseManual_Click(object sender, EventArgs e)
        {
            String selectedCourse=null;
            int selectedCount = 0;
            var selectedSciene = lst_Science.SelectedItem;
            var selectedTechnology = lst_Technology.SelectedItem;
            var selectedEngineering = lst_Engineering.SelectedItem;
            var selectedMath = lst_Math.SelectedItem;
            if ((selectedSciene == null) && (selectedTechnology == null) && (selectedEngineering == null) && (selectedMath == null))
            {
                lb_ChooseError.Text = "Please Choose one Item";
                lb_ChooseError.Visible = true;
                lb_ChooseError.BringToFront();
            }
            if (selectedSciene != null)
            {
                selectedCourse = (lst_Science.SelectedItem as SelectValue).Value;
                selectedCount++;
            }
            if (selectedTechnology != null)
            {
                selectedCourse = (lst_Technology.SelectedItem as SelectValue).Value;
                selectedCount++;
            }
            if (selectedEngineering != null)
            {
                selectedCourse = (lst_Engineering.SelectedItem as SelectValue).Value;
                selectedCount++;
            }
            if (selectedMath != null)
            {
                selectedCourse = (lst_Math.SelectedItem as SelectValue).Value;
                selectedCount++;
            }
            if (selectedCount > 1)
            {
                lb_ChooseError.Text = "Please select only one";
                lb_ChooseError.Visible = true;
                lb_ChooseError.BringToFront();
                lst_Science.ClearSelected();
                lst_Technology.ClearSelected();
                lst_Engineering.ClearSelected();
                lst_Math.ClearSelected();
            }
            else if (selectedCount!=0)
            {
                lb_ChooseError.Visible = false;
                GameCoding gameCoding = null;
                if (String.IsNullOrEmpty(tb_GroupName.Text))
                {
                    lb_ChooseError.Text = "Please choose group";
                    lb_ChooseError.Visible = true;
                }
                else
                {
                     gameCoding = gameCodingManager.LoadGameCoding(selectedCourse, tb_GroupName.Text);
                    UpdateStudentTakenCourses(gameCoding, selectedCourse);
                }
                if (lst_MoneyList.SelectedItem == null)
                {
                    lb_ChooseError.Text = "Please select from money list";
                    lb_ChooseError.Visible = true;
                }
                else
                {
                    UpdateMoney(lst_MoneyList.SelectedValue.ToString());
                }
                if (gameCoding != null)
                {
                    UpdateToyReservation(gameCoding.Game);
                    lb_ChooseError.Text = "Successfully Added";
                    lb_ChooseError.Visible = true;
                    clearallLists();
                    tbStudentName.Text = String.Empty;
                    tb_Age.Text = String.Empty;
                    tb_FingSearch.Text = String.Empty;
                    tb_GroupMan.Text = String.Empty;
                    tb_GroupName.Text = String.Empty;
                    tb_MobileSearch.Text = String.Empty;
                    tb_ParentNameShow.Text = String.Empty;
                    tb_StudentMobile.Text = String.Empty;
                    if (cb_Branch.Items.Count != 0)
                    {
                        cb_Branch.Items.Clear();
                    }
                    if (lst_KidsNames.Items.Count != 0)
                    {
                        lst_KidsNames.Items.Clear();
                    }
                    if (lst_groupName.Items.Count != 0)
                    {
                        lst_groupName.Items.Clear();
                    }
                    int i=0;
                    int gpIndexToRemove=-1;
                    for (i=0;i<runningGroups.Count;i++)
                    {
                        Group group = runningGroups[i];
                       
                        if (group.ageRange.ID == gameCoding.AgeRange.ID)
                        {
                            gpIndexToRemove = i;
                        }
                    }
                    if(gpIndexToRemove!=-1)
                    runningGroups.RemoveAt(gpIndexToRemove);
                }

            }
        }
        private void UpdateToyReservation(Games game)
        {
            DateTime startDateTime = dtp_SessionTime.Value;
            DateTime endDateTime = startDateTime.AddMinutes(30);
            gameCodingManager.AddToyReservation(startDateTime,endDateTime, game);
        }
        private void UpdateMoney(String description)
        {
            user = LoginForm.user;
            MoneyList moneylist = moneyListManager.LoadSelectedMoneyList(description);
            loginManager.updateCashPerUser(user, moneylist.Amount);

        }
        private void UpdateStudentTakenCourses(GameCoding gameCoding,String selectedCourse)
        {
            List<Student> listStudents = new List<Student>();
            listStudents = GetStudentsIngroup(tb_GroupName.Text);
            int start = selectedCourse.IndexOf("(") + 1;
            int end = selectedCourse.IndexOf(")") - start;
            Module module = gameCodingManager.GetSelectedModule(int.Parse(selectedCourse.Substring(start,end)), gameCoding.Game);
            foreach (Student student in listStudents)
            {
                gameCodingManager.SaveStudentTakenCousres(student,gameCoding,module);
            }
        }

        private List<Student> GetStudentsIngroup(string groupId)
        {
            List<Student> students = new List<Student>();
            foreach (Group runGroup in runningGroups)
            {
                if (runGroup.ageRange.ID == groupId)
                    students= runGroup.students;
            }
            return students;
        }

        private void cb_Track_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_Branch.Items.Clear();
            if (cb_Track.SelectedItem != null)
            {
                String selectedTrack = cb_Track.SelectedValue.ToString();
                List<Branches> branches = gameCodingManager.LoadBranchesPerStem(selectedTrack[0].ToString());

                foreach (Branches branch in branches)
                {
                    cb_Branch.Items.Add(branch.branchName);
                }
            }
        }

        private void cb_Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_Modules.Items.Clear();
            List<DomainModel.BusinessObject.GameCoding> gameCodingList = new List<GameCoding>();
            STEM stemDomain = new STEM();
            stemDomain.Id = cb_Track.SelectedValue.ToString()[0].ToString();
            AgeRange ageRange = GetAgeRange(cb_ageGroup.SelectedItem.ToString());
            List<Branches> branches = gameCodingManager.LoadBranchesPerStem(stemDomain.Id);
            Branches branch = new Branches();
            foreach (Branches branchItem in branches)
            {
                if (branchItem.branchName == cb_Branch.SelectedItem.ToString())
                    branch = branchItem;
            }
            List<Student> students = GetStudentsIngroup(tb_GroupMan.Text);
            List<int> excludedModulesIds = GetTakenModulesIdAllStudent(students);

            gameCodingList = gameCodingManager.LoadGameModuleByAgeAndSTEMAndBranch(ageRange, stemDomain,branch);
            List<SelectValue> gameNamesList = GetGameName(gameCodingList, excludedModulesIds);
            foreach (SelectValue name in gameNamesList)
            {
                lst_Modules.Items.Add(name.Text);
            }
        }

        private void btn_ChooseMan_Click(object sender, EventArgs e)
        {
            String selectedCourse = null;
            var selectedModule = (lst_Modules.SelectedItem as SelectValue).Value;
            if ((selectedModule == null))
            {
                lb_ChooseManError.Text = "Please Choose one Item";
                lb_ChooseManError.Visible = true;
                lb_ChooseManError.BringToFront();
            }
            else if (lst_Modules.Items.Count > 1)
            {
                lb_ChooseManError.Text = "Please Choose only one Item";
                lb_ChooseManError.Visible = true;
                lb_ChooseManError.BringToFront();
                lst_Modules.ClearSelected();
            }
            else
            {
                selectedCourse = (lst_Modules.SelectedItem as SelectValue).Value;
                lb_ChooseManError.Visible = false;
                GameCoding gameCoding=null;
                if (String.IsNullOrEmpty(tb_GroupMan.Text))
                {
                    lb_ChooseError.Text = "Please choose group";
                    lb_ChooseError.Visible = true;
                }
                else
                {
                     gameCoding = gameCodingManager.LoadGameCoding(selectedCourse, tb_GroupMan.Text);
                    UpdateStudentTakenCourses(gameCoding, selectedCourse);
                }
                if (lst_MoneyList.SelectedItem == null)
                {
                    lb_ChooseManError.Text = "Please select from money list";
                    lb_ChooseManError.Visible = true;
                    lb_ChooseManError.BringToFront();
                }
                else
                {
                    UpdateMoney(lst_MoneyList.SelectedValue.ToString());
                }
                if (gameCoding != null)
                {
                    UpdateToyReservation(gameCoding.Game);
                    lb_ChooseManError.Text = "Successfully Added";
                    lb_ChooseManError.Visible = true;
                    lb_ChooseManError.BringToFront();
                    clearallLists();
                    tbStudentName.Text = String.Empty;
                    tb_Age.Text = String.Empty;
                    tb_FingSearch.Text = String.Empty;
                    tb_GroupMan.Text = String.Empty;
                    tb_GroupName.Text = String.Empty;
                    tb_MobileSearch.Text = String.Empty;
                    tb_ParentNameShow.Text = String.Empty;
                    tb_StudentMobile.Text = String.Empty;
                    if (cb_Branch.Items.Count != 0)
                    {
                        cb_Branch.Items.Clear();
                    }
                    if (lst_KidsNames.Items.Count != 0)
                    {
                        lst_KidsNames.Items.Clear();
                    }
                    if (lst_groupName.Items.Count != 0)
                    {
                        lst_groupName.Items.Clear();
                    }
                    int i = 0;
                    int gpIndexToRemove = -1;
                    for (i = 0; i < runningGroups.Count; i++)
                    {
                        Group group = runningGroups[i];

                        if (group.ageRange.ID == gameCoding.AgeRange.ID)
                        {
                            gpIndexToRemove = i;
                        }
                    }
                    if (gpIndexToRemove != -1)
                        runningGroups.RemoveAt(gpIndexToRemove);
                }
            }
        }

        private void btn_SearchByPrint_Click(object sender, EventArgs e)
        {
            lb_ErrorSearch.Visible = false;

            lst_KidsNames.Items.Clear();
            students = new List<Student>();
            students = studentManager.GetStudentsByFingerPrint(tb_FingSearch.Text);
            if ((tb_FingSearch.Text == ""))
            {
                lb_ErrorSearch.Text = "Please Enter Finger Print";
                lb_ErrorSearch.Visible = true;
                tb_FingSearch.Text = "";
            }
            else if (students.Count == 0)
            {
                lb_ErrorSearch.Text = "Child not registered";
                lb_ErrorSearch.Visible = true;
                tb_FingSearch.Text = "";
            }
            else
            {
                foreach (Student student in students)
                {
                    lst_KidsNames.Items.Add(student.StudentName);
                }
            }

        }
    }
}
