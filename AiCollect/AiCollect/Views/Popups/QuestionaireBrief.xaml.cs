using AiCollect.Models;
using AiCollect.Services;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiCollect.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionaireBrief : PopupPage
    {
        private ConfigurationPageState cps;
        private Questionaire questionaire;
        private User user;
        private ObjectType ObjectType;
        public QuestionaireBrief(ObjectType objectType, Questionaire questionaire, ConfigurationPageState cps)
        {
            InitializeComponent();
            ObjectType = objectType;
            this.cps = cps;
            this.questionaire = questionaire;
            Init();
        }

        private void Init()
        {
            try
            {
                user = AiDataStore.GetUser();

                var answer = questionaire.Sections.FirstOrDefault().Questions.Find(x => x.QuestionText == "Name").Answers.FirstOrDefault();
                name.Text = answer.AnswerText;
                Question qn = questionaire.Sections.FirstOrDefault().Questions.Find(x => x.QuestionText == "Region");
                if (qn != null)
                    if (qn.Answers != null)
                    {
                        region.IsVisible = true;
                        region.Text = qn.EnumList.EnumValues.Find(r => r.Code == Convert.ToInt64(qn.Answers.FirstOrDefault())).Description;
                    }


                switch (ObjectType)
                {
                    case ObjectType.Purchase:
                        actionBtn.Text = "Purchase";
                        actionBtn.Clicked += OnPurchase;
                        break;
                    case ObjectType.Certification:
                        actionBtn.Text = "Certify";
                        actionBtn.Clicked += OnCertify;
                        break;
                    default:
                        actionBtn.Text = "Survey";
                        actionBtn.Clicked += OnSurvey;
                        break;
                }
            }
            catch(Exception ex)
            {

            }
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }

        private void OnSurvey(object sender, EventArgs args)
        {
            try
            {
                cps.OnFieldInspectionSelected();
            }
            catch (Exception ex)
            {
                //DisplayAlert("Error", ex.Message);
                Debug.WriteLine($"Exception after Selection Complete:  {ex}");
            }
        }

        private void OnPurchase(object sender, EventArgs args)
        {
            try
            {
                cps.OnPurchaseRoleSeleted();
            }
            catch(Exception ex)
            {

            }
        }

        private void OnCertify(object sender, EventArgs args)
        {
            cps.OnCertificationRoleSeleted();
        }

        private void OnCancel(object sender, EventArgs args)
        {
            try
            {
                Task.Run(async () => await PopupNavigation.Instance.RemovePageAsync(this));
            }
            catch (Exception ex)
            {
                //DisplayAlert("Error", ex.Message);
                Debug.WriteLine($"Exception after Selection Complete:  {ex}");
            }
        }
    }
}