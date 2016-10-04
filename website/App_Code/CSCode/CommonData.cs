using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for CommonData
/// </summary>
public class CommonData
{
    public static SortedList Gender()
    {
        SortedList Gender = new SortedList();
        Gender.Add(1, "M");
        Gender.Add(2, "F");
        Gender.Add(3,"Other");
        return Gender;
     
    }

    public static SortedList personalDetails()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "Person with a disability");
        Details.Add(2, "Parent or family member of a person with a disability");
        Details.Add(3, "Provider of services");
        Details.Add(4, "Executive of not for profit");
        Details.Add(5, "Employer");
        Details.Add(6, "Attorney");
        Details.Add(7, "Financial planner");
        Details.Add(8, "Benefits planner");
        Details.Add(9, "Government agency staff");
        //Details.Add(10, "Other");
        return Details;
               
    }
    public static SortedList financialServiceExperience()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "Have a checking account");
        Details.Add(2, "Have a savings account");
        Details.Add(3, "Member of a credit union");
        Details.Add(4, "Have a retirement account");
        Details.Add(5, "Have a trust");
        Details.Add(6, "Have a credit card(s) (non-debit)");
        Details.Add(7, "Have a debit card");
        Details.Add(8, "Have a loan");
        Details.Add(9, "Have an individual development account (IDA)");
        return Details;

    }

    public static SortedList employmentStatus()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "Not employed");
        Details.Add(2, "Work full time");
        Details.Add(3, "Work part time");        
        return Details;

    }
    public static SortedList assetExperiences()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "Own a home");
        Details.Add(2, "Have a financial plan");
        Details.Add(3, "Own a business");
        Details.Add(4, "Own mutual funds");
        Details.Add(5, "Have life insurance");
        return Details;

    }

    public static SortedList healthcareCoverageType()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "Private");
        Details.Add(2, "Public (Medicaid, Medicare)");
        Details.Add(3, "Insurance through employer");
        Details.Add(4, "COBRA");
        Details.Add(5, "Military/VA");
        return Details;

    }
    public static SortedList educationLevel()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "Did not attend/complete high school");
        Details.Add(2, "High school graduate or equivalent");
        Details.Add(3, "Some college");
        Details.Add(4, "College graduate");
        Details.Add(5, "Advanced degree");
        Details.Add(6, "Other");
        return Details;

    }

    public static SortedList maritalStatus()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "Single");
        Details.Add(2, "Married");
        Details.Add(3, "Separated/Divorced");
        Details.Add(4, "Other Domestic Partnership");        
        return Details;

    }

    public static SortedList livingArrangement()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "Live alone");
        Details.Add(2, "Live with family or others");
        return Details;

    }

    public static SortedList Age()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "under 18");
        Details.Add(2, "18-30");
        Details.Add(3, "31-40");
        Details.Add(4, "41-50");
        Details.Add(5, "51-65");
        Details.Add(6, "65+");
        return Details;

    }
    
    public static SortedList areaGreatestIntrest()
    {
        SortedList Details = new SortedList();
        Details.Add(1, "Public benefits");
        Details.Add(2, "Tax benefits");
        Details.Add(3, "Financial planning");
        Details.Add(4, "Trusts");
        Details.Add(5, "Managing credit");
        Details.Add(6, "Starting and growing a small business");
        Details.Add(7, "Ticket to Work");
        Details.Add(8, "Individual development accounts");
        Details.Add(9, "Insurance");
        Details.Add(10, "Returning to work");
        return Details;

    }




}
