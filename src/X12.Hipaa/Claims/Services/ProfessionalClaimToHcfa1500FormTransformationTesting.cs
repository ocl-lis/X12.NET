namespace X12.Hipaa.Claims.Services
{
    using System;
    using System.Collections.Generic;

    using X12.Hipaa.Claims.Forms;
    using X12.Hipaa.Claims.Forms.Professional;
    using X12.Hipaa.Enums;

    /// <summary>
    /// Provides a transformer for <see cref="Claim"/> objects to HCFA1500 claims
    /// </summary>
    public class ProfessionalClaimToHcfa1500FormTransformationTesting : ProfessionalClaimToHcfa1500FormTransformation
    {
        private readonly string formImagePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfessionalClaimToHcfa1500FormTransformation"/> class
        /// </summary>
        /// <param name="formImagePath">Form image path to be transformed</param>
        public ProfessionalClaimToHcfa1500FormTransformationTesting(string formImagePath) : base(formImagePath)
        {
            this.formImagePath = formImagePath;
        }



        /// <summary>
        /// Transforms HCFA1500 claim data to form pages
        /// </summary>
        /// <param name="hcfa"><see cref="HCFA1500Claim"/> data to be transformed</param>
        /// <returns>Collection of <see cref="FormPage"/> objects</returns>
        public override List<FormPage> TransformHcfa1500ToFormPages(HCFA1500Claim hcfa)
        {
            var pages = new List<FormPage>();
            FormPage page = null;
            for (int i = 0; i < hcfa.Field24_ServiceLines.Count; i++)
            {
                if (i % 6 == 0)
                {
                    page = new FormPage();
                    pages.Add(page);
                    page.MasterReference = "hcfa1500";
                    page.ImagePath = this.formImagePath;

                    // Render header
                    // LINE 1
                    //AddBlock(page, 4, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsMedicare));
                    //AddBlock(page, 11, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsMedicaid));
                    //AddBlock(page, 18, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsTricareChampus));
                    //AddBlock(page, 27, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsChampVa));
                    //AddBlock(page, 34, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsGroupHealthPlan));
                    //AddBlock(page, 42, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsFECABlkLung));
                    //AddBlock(page, 48, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsOther));

                    AddBlock(page, 3.9m, 6.6m, 2, ConditionalMarker(true));
                    AddBlock(page, 11, 6.9m, 2, ConditionalMarker(true));
                    AddBlock(page, 18.1m, 6.9m, 2, ConditionalMarker(true));
                    AddBlock(page, 26.7m, 6.9m, 2, ConditionalMarker(true));
                    AddBlock(page, 34, 6.9m, 2, ConditionalMarker(true));
                    AddBlock(page, 42, 6.9m, 2, ConditionalMarker(true));
                    AddBlock(page, 47.8m, 6.9m, 2, ConditionalMarker(true));

                    AddBlock(page, 54m, 6.8m, 30, hcfa.Field01a_InsuredsIDNumber);

                    // LINE 2
                    AddBlock(page, 4, 9, 28.5m, hcfa.Field02_PatientsName);

                    AddBlock(page, 34.2m, 9.1m, 3, "01");//AddBlock(page, 34, 9, 3, hcfa.Field03_PatientsDateOfBirth.Month);
                    AddBlock(page, 37.2m, 9.1m, 3, "01");// hcfa.Field03_PatientsDateOfBirth.Day);
                    AddBlock(page, 40.5m, 9.1m, 3, "20");// hcfa.Field03_PatientsDateOfBirth.Year);

                    AddBlock(page, 44.9m, 9.3m, 2.8m, ConditionalMarker(true), TextAlign.center);// ConditionalMarker(hcfa.Field03_PatientsSexMale), TextAlign.center);
                    AddBlock(page, 49.8m, 9.3m, 2.8m, ConditionalMarker(true), TextAlign.center); //ConditionalMarker(hcfa.Field03_PatientsSexFemale), TextAlign.center);
                    AddBlock(page, 54, 9, 30, hcfa.Field04_InsuredsName);

                    // LINE 3
                    AddBlock(page, 4, 11.1m, 28.5m, hcfa.Field05_PatientsAddress_Street);
                    AddBlock(page, 36.2m, 11.2m, 2, ConditionalMarker(true));// ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsSelf));
                    AddBlock(page, 41, 11.2m, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsSpouseOf));
                    AddBlock(page, 45.6m, 11.2m, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsChildOf));
                    AddBlock(page, 50.2m, 11.2m, 2, ConditionalMarker(true));// ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsOther));
                    AddBlock(page, 54, 11, 30, hcfa.Field07_InsuredsAddress_Street);

                    // LINE 4
                    AddBlock(page, 4, 13.2m, 25, hcfa.Field05_PatientsAddress_City);
                    AddBlock(page, 28.8m, 13, 3.5m, hcfa.Field05_PatientsAddress_State);

                    // Field 8 unused in CMS-1500
                    //AddBlock(page, 38, 13, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsSingle));
                    //AddBlock(page, 44, 13, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsMarried));
                    //AddBlock(page, 50, 13, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsOther));

                    AddBlock(page, 54, 13, 23, hcfa.Field07_InsuredsAddress_City);
                    AddBlock(page, 79.7m, 13, 6, hcfa.Field07_InsuredsAddress_State);

                    // LINE 5
                    AddBlock(page, 4, 15, 13, hcfa.Field05_PatientsAddress_Zip);
                    AddBlock(page, 18, 15, 14.5m, "(000)555-55555");//hcfa.Field05_PatientsTelephone);

                    // Field 8 unused in CMS-1500
                    //AddBlock(page, 38, 15, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsEmployed));
                    //AddBlock(page, 44, 15.6m, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsFullTimeStudent));
                    //AddBlock(page, 50, 16, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsPartTimeStudent));

                    AddBlock(page, 54, 15, 12, hcfa.Field07_InsuredsAddress_Zip);
                    AddBlock(page, 68.5m, 15, 14.5m, "(000)555-55555"); //hcfa.Field07_InsuredsPhoneNumber);

                    // LINE 6
                    AddBlock(page, 4, 17, 28.5m, "OTHER INSURED");// hcfa.Field09_OtherInsuredsName);
                    AddBlock(page, 53, 17, 30, "INSURED GROUP");// hcfa.Field11_InsuredsPolicyGroupOfFECANumber);

                    // LINE 7
                    AddBlock(page, 4, 19, 28.5m, "OTHER INSURED GROUP");// hcfa.Field09a_OtherInsuredsPolicyOrGroup);

                    AddBlock(page, 38.25m, 19.05M, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field10a_PatientConditionRelatedToEmployment));
                    AddBlock(page, 44, 19.05M, 2, ConditionalMarker(true));//ConditionalMarker(!hcfa.Field10a_PatientConditionRelatedToEmployment));

                    AddBlock(page, 54, 19.5M, 3, hcfa.Field11a_InsuredsDateOfBirth.Month);
                    AddBlock(page, 58, 19.5m, 3, hcfa.Field11a_InsuredsDateOfBirth.Day);
                    AddBlock(page, 62, 19.5m, 3, hcfa.Field11a_InsuredsDateOfBirth.Year);

                    AddBlock(page, 72m, 19.65m, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field11a_InsuredsSexIsMale), TextAlign.center);
                    AddBlock(page, 78.8m, 19.65m, 2, ConditionalMarker(true));// ConditionalMarker(hcfa.Field11a_InsuredsSexIsFemale), TextAlign.center);

                    // LINE 8
                    // Field 9b is not supplied by 837P data.
                    //estos datos no se donde van en esta version del reporte
                    //AddBlock(page, 5, 21, 3, "01");// hcfa.Field09b_OtherInsuredsDateOfBirth.Month);
                    //AddBlock(page, 8, 21, 3, "01");// hcfa.Field09b_OtherInsuredsDateOfBirth.Day);
                    //AddBlock(page, 11, 21, 3, "01");// hcfa.Field09b_OtherInsuredsDateOfBirth.Year);
                    //AddBlock(page, 21, 21, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field09b_OtherInsuredIsMale));
                    //AddBlock(page, 27, 21, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field09b_OtherInsuredIsFemale));

                    AddBlock(page, 38.25m, 21.5m, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field10b_PatientConditionRelatedToAutoAccident));
                    AddBlock(page, 44, 21.5m, 2, ConditionalMarker(true));//ConditionalMarker(!hcfa.Field10b_PatientConditionRelatedToAutoAccident));
                    AddBlock(page, 48, 21.5m, 2.5m, "00");// hcfa.Field10b_PatientConditionRelToAutoAccidentState);
                    AddBlock(page, 53, 21, 30, hcfa.Field11b_InsuredsEmployerOrSchool);

                    // LINE 9
                    AddBlock(page, 4, 23, 28.5m, hcfa.Field09c_OtherInsuredsEmployerNameOrSchoolName);
                    AddBlock(page, 38.25m, 23.8m, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field10c_PatientConditionRelatedToOtherAccident));
                    AddBlock(page, 44, 23.8M, 2, ConditionalMarker(true));//ConditionalMarker(!hcfa.Field10c_PatientConditionRelatedToOtherAccident));
                    AddBlock(page, 54, 23.7M, 30, hcfa.Field11c_InsuredsPlanOrProgramName);

                    // LINE 10
                    AddBlock(page, 4, 25.55m, 28.5m, "INSURED PLAN NAME");// hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName);
                    AddBlock(page, 33.5m, 25.55M, 20, "RESERVE FOR LOCAL USE");// hcfa.Field10d_ReservedForLocalUse);
                    AddBlock(page, 55, 25.8m, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field11d_IsThereOtherHealthBenefitPlan));
                    AddBlock(page, 59.8M, 25.8m, 2, ConditionalMarker(true));//ConditionalMarker(!hcfa.Field11d_IsThereOtherHealthBenefitPlan));

                    // LINE 11
                    AddBlock(page, 9, 29, 25, hcfa.Field12_PatientsOrAuthorizedSignature, TextAlign.center);
                    AddBlock(page, 39, 29, 14, hcfa.Field12_PatientsOrAuthorizedSignatureDate.ToString(), TextAlign.center);
                    AddBlock(page, 59, 29, 24, hcfa.Field13_InsuredsOrAuthorizedSignature, TextAlign.center);

                    // LINE 12 (todo(incluir))
                    //if (hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy != null)
                    //{
                    AddBlock(page, 5, 31.25M, 3, "01");// hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy.Month);
                    AddBlock(page, 8, 31.25M, 3, "01");//hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy.Day);
                    AddBlock(page, 11.3M, 31.25M, 3, "01");//hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy.Year);
                    //}

                    // Field 15 
                    AddBlock(page, 40.5M, 31.25M, 3, "02");//hcfa.Field15_DatePatientHadSameOrSimilarIllness.Month);
                    AddBlock(page, 43.5M, 31.25M, 3, "02");// hcfa.Field15_DatePatientHadSameOrSimilarIllness.Day);
                    AddBlock(page, 46.5M, 31.25M, 3, "02");//hcfa.Field15_DatePatientHadSameOrSimilarIllness.Year);

                    //if (hcfa.Field16_DatePatientUnableToWork_Start != null)
                    //{
                    AddBlock(page, 58.5M, 31.25M, 3, "03");//hcfa.Field16_DatePatientUnableToWork_Start.Month);
                    AddBlock(page, 61.5M, 31.25M, 3, "03");// hcfa.Field16_DatePatientUnableToWork_Start.Day);
                    AddBlock(page, 64.5M, 31.25M, 3, "03");//hcfa.Field16_DatePatientUnableToWork_Start.Year);
                                                    //}

                    //if (hcfa.Field16_DatePatientUnableToWork_End != null)
                    //{
                    AddBlock(page, 74.7m, 31.25M, 3, "04");//hcfa.Field16_DatePatientUnableToWork_End.Month);
                    AddBlock(page, 78, 31.25M, 3, "04");//hcfa.Field16_DatePatientUnableToWork_End.Day);
                    AddBlock(page, 82.2m, 31.25M, 3, "04");//hcfa.Field16_DatePatientUnableToWork_End.Year);
                    //}

                    // LINE 13
                    AddBlock(page, 7, 33, 26, hcfa.Field17_ReferringProviderOrOtherSource_Name);
                    AddBlock(page, 33, 32.3m, 3, "Q1");// hcfa.Field17a_OtherID_Qualifier);
                    AddBlock(page, 36, 32.4m, 16, "123498");/// hcfa.Field17a_OtherID_Number);
                    AddBlock(page, 36, 33.5m, 16, "NPI123");// hcfa.Field17b_NationalProviderIdentifier);

                    // Field 18
                    AddBlock(page, 58.5m, 33.4M, 3, "05");//hcfa.Field18_HospitalizationDateFrom.Month);
                    AddBlock(page, 61.5m, 33.4M, 3, "05");//hcfa.Field18_HospitalizationDateFrom.Day);
                    AddBlock(page, 64.5m, 33.4M, 3, "05");//hcfa.Field18_HospitalizationDateFrom.Year);

                    AddBlock(page, 74.5m, 33.4M, 3, "06");//hcfa.Field18_HospitalizationDateTo.Month);
                    AddBlock(page, 78, 33.4M, 3, "06");//hcfa.Field18_HospitalizationDateTo.Day);
                    AddBlock(page, 82.2m, 33.4M, 3, "06");// hcfa.Field18_HospitalizationDateTo.Year);

                    // LINE 14
                    // We limit the length of the remark to only the size of the block. 
                    if (hcfa.Field19_ReservedForLocalUse != null && hcfa.Field19_ReservedForLocalUse.Length > 58)
                    {
                        AddBlock(page, 4, 35, 49, hcfa.Field19_ReservedForLocalUse.Substring(0, 58));
                    }
                    else
                    {
                        AddBlock(page, 4, 35, 49, hcfa.Field19_ReservedForLocalUse);
                    }

                    AddBlock(page, 55m, 35.5m, 2, ConditionalMarker(true));//ConditionalMarker(hcfa.Field20_OutsideLab));
                    AddBlock(page, 59m, 35.5m, 2, ConditionalMarker(true));//ConditionalMarker(!hcfa.Field20_OutsideLab));

                    AddBlock(page, 65, 35, 9, hcfa.Field20_OutsideLab ? Convert.ToString(hcfa.Field20_OutsideLabCharges) : string.Empty, TextAlign.right);

                    // Note, we do not use second charge box at all here.
                    AddBlock(page, 74, 35, 9, string.Empty, TextAlign.right);

                    // Line 15
                    AddBlock(page, 6.5m, 37.4m, 8, hcfa.Field21_Diagnosis1);
                    AddBlock(page, 6.5m, 38.5m, 8, hcfa.Field21_Diagnosis5);
                    AddBlock(page, 6.5m, 39.6m, 8, hcfa.Field21_Diagnosis9);


                    AddBlock(page, 18.7m, 37.4m, 8, hcfa.Field21_Diagnosis2);
                    AddBlock(page, 18.7m, 38.5m, 8, hcfa.Field21_Diagnosis6);
                    AddBlock(page, 18.7m, 39.6m, 8, hcfa.Field21_Diagnosis10);


                    AddBlock(page, 30.9m, 37.4m, 8, hcfa.Field21_Diagnosis3);
                    AddBlock(page, 30.9m, 38.5m, 8, hcfa.Field21_Diagnosis7);
                    AddBlock(page, 30.9m, 39.6m, 8, hcfa.Field21_Diagnosis11);

                    AddBlock(page, 43, 37.4m, 8, hcfa.Field21_Diagnosis4);
                    AddBlock(page, 43, 38.5m, 8, hcfa.Field21_Diagnosis8);
                    AddBlock(page, 43, 39.6m, 8, hcfa.Field21_Diagnosis12);


                    AddBlock(page, 54, 37.8m, 11, "SB CODE");// hcfa.Field22_MedicaidSubmissionCode);
                    AddBlock(page, 68.5m, 37.8m, 18, "ORI REF NUM");// hcfa.Field22_OriginalReferenceNumber);

                    // Line 16









                    AddBlock(page, 53, 39, 30, hcfa.Field23_PriorAuthorizationNumber);
                }

                // Render service lines
                decimal y = 42.8m + 2 * (i % 6);
                var line = hcfa.Field24_ServiceLines[i];
                AddBlock(page, 4, y, 60, line.CommentLine);
                AddBlock(page, 68, y, 3, line.RenderingProviderIdQualifier);
                AddBlock(page, 71, y, 12, line.RenderingProviderId);

                if (line.DateFrom != null)
                {
                    AddBlock(page, 4, y + 1, 3, line.DateFrom.Month);
                    AddBlock(page, 7, y + 1, 3, line.DateFrom.Day);
                    AddBlock(page, 9.6m, y + 1, 3, line.DateFrom.Year);
                }
                else
                {
                    AddBlock(page, 4, y + 1, 3, string.Empty);
                    AddBlock(page, 7, y + 1, 3, string.Empty);
                    AddBlock(page, 9.6m, y + 1, 3, string.Empty);
                }

                if (line.DateTo != null)
                {
                    AddBlock(page, 12, y + 1, 3, line.DateTo.Month);
                    AddBlock(page, 15, y + 1, 3, line.DateTo.Day);
                    AddBlock(page, 17.3M, y + 1, 3, line.DateTo.Year);
                }
                else
                {
                    AddBlock(page, 12, y + 1, 3, string.Empty);
                    AddBlock(page, 15, y + 1, 3, string.Empty);
                    AddBlock(page, 17.3M, y + 1, 3, string.Empty);
                }

                AddBlock(page, 22, y + 1, 3, "3");// line.PlaceOfService);
                AddBlock(page, 24, y + 1, 2, "4");// line.EmergencyIndicator);
                AddBlock(page, 28, y + 1, 6, line.ProcedureCode);
                AddBlock(page, 35, y + 1, 3, line.Mod1);
                AddBlock(page, 38, y + 1, 3, line.Mod2);
                AddBlock(page, 41, y + 1, 3, line.Mod3);
                AddBlock(page, 43.5m, y + 1, 3, line.Mod4);
                AddBlock(page, 47, y + 1, 2, line.DiagnosisPointer1);
                AddBlock(page, 48, y + 1, 2, line.DiagnosisPointer2);
                AddBlock(page, 49, y + 1, 2, line.DiagnosisPointer3);
                AddBlock(page, 50, y + 1, 2, line.DiagnosisPointer4);
                AddBlock(page, 55m, y + 1, 9, $"{line.Charges:0.00}".Replace(".", " "), TextAlign.right);
                AddBlock(page, 62, y + 1, 4, $"{line.DaysOrUnits}", TextAlign.right);
                AddBlock(page, 68, y + 1, 2, "7");// line.EarlyPeriodicScreeningDiagnosisAndTreatment);
                AddBlock(page, 73, y + 1, 12, "123456");// line.RenderingProviderNpi);

                // Footer
                if (i % 6 == 5 || i == hcfa.Field24_ServiceLines.Count - 1)
                {
                    // Render footer
                    AddBlock(page, 4, 55.5M, 15, hcfa.Field25_FederalTaxIDNumber);
                    if (hcfa.Field25_IsSSN)
                    {
                        AddBlock(page, 19.8m, 55.7M, 2, "X");
                    }

                    if (hcfa.Field25_IsEIN)
                    {
                        AddBlock(page, 22, 55.7M, 2, "X");
                    }

                    AddBlock(page, 26, 55.5m, 14, hcfa.Field26_PatientAccountNumber);
                    if (hcfa.Field27_AcceptAssignment.HasValue)
                    {
                        AddBlock(
                            page,
                            hcfa.Field27_AcceptAssignment.Value ? 40.6m : 46,
                            55.8m,
                            2,
                            "X");
                    }

                    AddBlock(page, 54.5M, 55.5M, 9, $"{hcfa.Field28_TotalCharge:0.00}".Replace(".", " "), TextAlign.right);
                    AddBlock(page, 64.4m, 55.5m, 9, $"{12.52:0.00}".Replace(".", " "), TextAlign.right);
                    //AddBlock(page, 74, 55, 9, $"{hcfa.Field30_BalanceDue:0.00}".Replace(".", " "), TextAlign.right);

                    // Box 31
                    if (hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile.HasValue)
                    {
                        AddBlock(page, 4, 59.5m, 21, "PROVIDER SIGNATURE", TextAlign.center);
                        AddBlock(
                            page,
                            4,
                            60.2m,
                            21,
                            hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile.Value ? "IS ON FILE" : "NOT ON FILE",
                            TextAlign.center);
                    }

                    // Box 32
                    AddBlock(page, 26, 58, 27, hcfa.Field32_ServiceFacilityLocation_Name);
                    AddBlock(page, 26, 59, 27, hcfa.Field32_ServiceFacilityLocation_Street);
                    AddBlock(page, 26, 60, 27, $"{hcfa.Field32_ServiceFacilityLocation_City}, {hcfa.Field32_ServiceFacilityLocation_State} {hcfa.Field32_ServiceFacilityLocation_Zip}");
                    AddBlock(page, 26, 61.3m, 10, hcfa.Field32a_ServiceFacilityLocation_Npi);
                    AddBlock(page, 38, 61.3m, 15, hcfa.Field32b_ServiceFacilityLocation_OtherID);

                    // Box 33
                    AddBlock(page, 69, 57, 27, "(555) 555-55555");// hcfa.Field33_BillingProvider_TelephoneNumber);
                    AddBlock(page, 54, 58, 27, hcfa.Field33_BillingProvider_Name);
                    AddBlock(page, 54, 59, 27, hcfa.Field33_BillingProvider_Street);
                    AddBlock(page, 54, 60, 27, $"{hcfa.Field33_BillingProvider_City}, {hcfa.Field33_BillingProvider_State} {hcfa.Field33_BillingProvider_Zip}");
                    AddBlock(page, 54, 61.3m, 10, hcfa.Field33a_BillingProvider_Npi);
                    AddBlock(page, 65m, 61.3m, 15, hcfa.Field33b_BillingProvider_OtherID);
                }
            }

            return pages;
        }



        private static FormDate FormatFormDate(DateTime? dateTime)
        {
            return new FormDate
            {
                Month = $"{dateTime:MM}",
                Day = $"{dateTime:dd}",
                Year = $"{dateTime:yy}"
            };
        }

        private static void LimitFieldWidths(HCFA1500Claim hcfa)
        {
            hcfa.Field01a_InsuredsIDNumber = SetStringLength(hcfa.Field01a_InsuredsIDNumber, 35);
            hcfa.Field02_PatientsName = SetStringLength(hcfa.Field02_PatientsName, 28);
            hcfa.Field04_InsuredsName = SetStringLength(hcfa.Field04_InsuredsName, 30);
            hcfa.Field05_PatientsAddress_Street = SetStringLength(hcfa.Field05_PatientsAddress_Street, 28);
            hcfa.Field05_PatientsAddress_City = SetStringLength(hcfa.Field05_PatientsAddress_City, 29);
            hcfa.Field05_PatientsAddress_Zip = SetStringLength(hcfa.Field05_PatientsAddress_Zip, 14);
            hcfa.Field07_InsuredsAddress_Street = SetStringLength(hcfa.Field07_InsuredsAddress_Street, 35);
            hcfa.Field07_InsuredsAddress_City = SetStringLength(hcfa.Field07_InsuredsAddress_City, 28);
            hcfa.Field07_InsuredsAddress_Zip = SetStringLength(hcfa.Field07_InsuredsAddress_Zip, 14);
            hcfa.Field09_OtherInsuredsName = SetStringLength(hcfa.Field09_OtherInsuredsName, 28);
            hcfa.Field09a_OtherInsuredsPolicyOrGroup = SetStringLength(hcfa.Field09a_OtherInsuredsPolicyOrGroup, 28);
            hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName = SetStringLength(hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName, 28);
            hcfa.Field11_InsuredsPolicyGroupOfFECANumber = SetStringLength(hcfa.Field11_InsuredsPolicyGroupOfFECANumber, 35);
            hcfa.Field11c_InsuredsPlanOrProgramName = SetStringLength(hcfa.Field11c_InsuredsPlanOrProgramName, 35);
            hcfa.Field17_ReferringProviderOrOtherSource_Name = SetStringLength(hcfa.Field17_ReferringProviderOrOtherSource_Name, 26);
            hcfa.Field17a_OtherID_Qualifier = SetStringLength(hcfa.Field17a_OtherID_Qualifier, 3);
            hcfa.Field17a_OtherID_Number = SetStringLength(hcfa.Field17a_OtherID_Number, 16);
            hcfa.Field17b_NationalProviderIdentifier = SetStringLength(hcfa.Field17b_NationalProviderIdentifier, 16);
            hcfa.Field22_MedicaidSubmissionCode = SetStringLength(hcfa.Field22_MedicaidSubmissionCode, 11);
            hcfa.Field22_OriginalReferenceNumber = SetStringLength(hcfa.Field22_OriginalReferenceNumber, 18);
            hcfa.Field23_PriorAuthorizationNumber = SetStringLength(hcfa.Field23_PriorAuthorizationNumber, 30);

            foreach (var line in hcfa.Field24_ServiceLines)
            {
                line.RenderingProviderNpi = SetStringLength(line.RenderingProviderNpi, 12);
            }

            hcfa.Field25_FederalTaxIDNumber = SetStringLength(hcfa.Field25_FederalTaxIDNumber, 15);
            hcfa.Field26_PatientAccountNumber = SetStringLength(hcfa.Field26_PatientAccountNumber, 14);
            hcfa.Field32_ServiceFacilityLocation_Name = SetStringLength(hcfa.Field32_ServiceFacilityLocation_Name, 31);
            hcfa.Field32_ServiceFacilityLocation_Street = SetStringLength(hcfa.Field32_ServiceFacilityLocation_Street, 31);
            hcfa.Field32_ServiceFacilityLocation_City = SetStringLength(hcfa.Field32_ServiceFacilityLocation_City, 16);
            hcfa.Field32_ServiceFacilityLocation_State = SetStringLength(hcfa.Field32_ServiceFacilityLocation_State, 2);
            hcfa.Field32_ServiceFacilityLocation_Zip = SetStringLength(hcfa.Field32_ServiceFacilityLocation_Zip, 10);
            hcfa.Field32a_ServiceFacilityLocation_Npi = SetStringLength(hcfa.Field32a_ServiceFacilityLocation_Npi, 11);
            hcfa.Field32b_ServiceFacilityLocation_OtherID = SetStringLength(hcfa.Field32b_ServiceFacilityLocation_OtherID, 17);
            hcfa.Field33_BillingProvider_Name = SetStringLength(hcfa.Field33_BillingProvider_Name, 35);
            hcfa.Field33_BillingProvider_Street = SetStringLength(hcfa.Field33_BillingProvider_Street, 31);
            hcfa.Field33_BillingProvider_City = SetStringLength(hcfa.Field33_BillingProvider_City, 19);
            hcfa.Field33_BillingProvider_State = SetStringLength(hcfa.Field33_BillingProvider_State, 2);
            hcfa.Field33_BillingProvider_Zip = SetStringLength(hcfa.Field33_BillingProvider_Zip, 10);
            hcfa.Field33a_BillingProvider_Npi = SetStringLength(hcfa.Field33a_BillingProvider_Npi, 10);
        }

        private static string SetStringLength(string source, int limit)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            return source.Length > limit ? source.Substring(0, limit) : source;
        }

        /// <summary>
        /// Used for filling in the CMS 1500 form where X's are placed where true
        /// </summary>
        /// <param name="b">Flag if marker should return "X"</param>
        /// <returns>"X" if the boolean is true, "" otherwise</returns>
        private static string ConditionalMarker(bool b)
        {
            return b ? "X" : string.Empty;
        }

        private static FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text, TextAlign textAlign = TextAlign.left)
        {
            const decimal XScale = 0.1m;
            const decimal YScale = 0.1685m;
            var block = new FormBlock
            {
                TextAlign = textAlign,
                Left = -0.21m + XScale * x,
                Top = 0.1m + YScale * y,
                Width = XScale * width,
                Height = YScale * 1.1m,
                Text = text
            };
            page.Blocks.Add(block);
            return block;
        }
    }
}
