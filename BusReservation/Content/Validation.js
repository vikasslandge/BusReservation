
//Login Validation
function validateLogin(){
    var mob = document.getElementById("MobileNumber").value;
    var pass = document.getElementById("Password").value;

    var mob_pattern= /^[7-9]{1}[0-9]{9}/; //start with 7/8/9 and remaining 9 will 
    var pass_pattern=/^((?=.*\d)(?=.*[Aa-Zz])).{4,10}/; //number 0-9 with 1 char between 4-10 digits

    if (mob == "") {
        document.getElementById("mob_error").innerHTML = "*Mobile Number is required";
        return false;
    }
    if (mob != "") {
        document.getElementById("mob_error").innerHTML = "";

    }
    if (pass == "") {
        document.getElementById("password_error").innerHTML = "*please enter the password";
        return false;
    }
    if (pass != "") {
        document.getElementById("password_error").innerHTML = "";

    }

   
    else {
        return true;
    }
}

//Search Validation
function validateSearch(){
    var source = document.getElementById("Source").value;
    var destination = document.getElementById("Destination").value;
    var date=document.getElementById("DateOfJourney").value;

    if (source == "") {
        document.getElementById("source_error").innerHTML = "*Please select the source";
        return false;
    }
    if (source != "") {
        document.getElementById("source_error").innerHTML = "";

    }
    if (destination == "") {
        document.getElementById("destination_error").innerHTML = "*Please select the destination";
        return false;
    }
    if (destination != "") {
        document.getElementById("destination_error").innerHTML = "";
    }

    if (date == "") {
        document.getElementById("date_error").innerHTML = "*Please select the date of journey";
        return false;
    }
    if (date != "") {
        document.getElementById("date_error").innerHTML = "";

    }
    else {
        return true;
    }
}

//Register Validation

function validateRegister(){
            var rname=document.getElementById("Name").value;
            var email=document.getElementById("Email").value;
            var password=document.getElementById("Password").value;
            var MobileNumber=document.getElementById("MobileNumber").value;
            var gender=document.getElementById("Gender").value;
            var dob=document.getElementById("DateOfBirth").value;
            var address=document.getElementById("Address").value;

            var pass_pattern=/^((?=.*\d)(?=.*[Aa-Zz])).{4,10}/; //number 0-9 with 1 char between 4-10 digits
            var mob_pattern= /^[7-9]{1}[0-9]{9}/; //start with 7/8/9 and remaining 9 will 
            var email_pattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;//shubham.kadam@gmail.com

if (rname == "") {
    document.getElementById("rname_error").innerHTML = "*This field is required";
    return false;
    }
if (rname != "") {
    document.getElementById("rname_error").innerHTML = "";
    }

if (email == "") {
    document.getElementById("email_error").innerHTML = "*This field is required";
    return false;
    }
if (email != "") {
    document.getElementById("email_error").innerHTML = "";
    }

if (password == "") {
    document.getElementById("password_error").innerHTML = "*This field is required";
    return false;
    }
if (password != "") {
    document.getElementById("password_error").innerHTML = "";
    }

if (MobileNumber == "") {
    document.getElementById("MobileNumber_error").innerHTML = "*This field is required";
    return false;
    }
if (MobileNumber != "") {
    document.getElementById("MobileNumber_error").innerHTML = "";
    }

if (gender == "") {
    document.getElementById("gender_error").innerHTML = "*This field is required";
    return false;
    }
if (gender != "") {
    document.getElementById("gender_error").innerHTML = "";
    }

if (dob == "") {
    document.getElementById("dob_error").innerHTML = "*This field is required";
    return false;
    }
if (dob != "") {
    document.getElementById("dob_error").innerHTML = "";
    }

if (address == "") {
    document.getElementById("address_error").innerHTML = "*This field is required";
    return false;
    }
if (address != "") {
    document.getElementById("address_error").innerHTML = "";
    }

    if(!mob_pattern.test(MobileNumber)){
        document.getElementById("MobileNumber_error").innerHTML="*Mobile number is not correct";
        return false;
    }
    if(!email_pattern.test(email)){
        document.getElementById("email_error").innerHTML="*Email is not in correct format";
        return false; 
    }
    if(!pass_pattern.test(mob)){
        document.getElementById("password_error").innerHTML="*Password is not in correct format";
        return false;
    }
    if(!NaN(rname)){
        document.getElementById("rname_error").innerHTML="*name should be character only";
        return false;
    }
    
}


//Passenger Validation
function validatePassenger(){
    var name = document.getElementById("Name").value;
    var age = document.getElementById("Age").value;
    var gender = document.getElementById("Gender").value;
    var contact = document.getElementById("Phone").value;
    
    var mob_pattern= /^[7-9]{1}[0-9]{9}/; //start with 7/8/9 and remaining 9 will 

    if (name == "") {
        document.getElementById("name_error").innerHTML = "*This field is required";
        return false;
    }
    if (name != "") {
        document.getElementById("name_error").innerHTML = "";
    }

    if (age == "") {
        document.getElementById("age_error").innerHTML = "*This field is required";
        return false;
    }
    if (age != "") {
        document.getElementById("age_error").innerHTML = "";
    }

    if (gender == "") {
        document.getElementById("gender_error").innerHTML = "*This field is required";
        return false;
    }
    if (gender != "") {
        document.getElementById("gender_error").innerHTML = "";
    }

    if (contact == "") {
        document.getElementById("contact_error").innerHTML = "*This field is required";
        return false;
    }
    if (contact != "") {
        document.getElementById("contact_error").innerHTML = "";
    }

    if(!mob_pattern.test(contact)){
        document.getElementById("contact_error").innerHTML="*Mobile number is not correct";
        return false;
    }
    if(!NaN(name)){
        document.getElementById("name_error").innerHTML="*name should be character only";
        return false;
    }
}

//Provider Validation
function validateProvider(){
    var orgName = document.getElementById("OrganizationName").value;
    var mobile = document.getElementById("MobileNumber").value;
    var mob_pattern= /^[7-9]{1}[0-9]{9}/; //start with 7/8/9 and remaining 9 will 
    if (orgName == "") {
        document.getElementById("organization_error").innerHTML = "*This field is required";
        return false;
    }
    if (orgName != "") {
        document.getElementById("organization_error").innerHTML = "";

    }
    if (mobile == "") {
        document.getElementById("mobile_error").innerHTML = "*This field is required";
        return false;
    }
    if (mobile != "") {
        document.getElementById("mobile_error").innerHTML = "";

    }
    if(!mob_pattern.test(mobile)){
        document.getElementById("mobile_error").innerHTML="*Mobile number is not correct";
        return false;
    }
    if(!NaN(orgName)){
        document.getElementById("organization_error").innerHTML="*Name should be character only";
        return false;
    }
    else {
        return true;
    }
}

//Offers Validation
function validateOffer(){
    var offercode = document.getElementById("OfferCode").value;
    var discount = document.getElementById("DiscountPercentage").value;
    var minAmount = document.getElementById("MinAmount").value;
    var expDate = document.getElementById("ExpiryDate").value;

    var offer_pattern=/^[Aa-Zz]{6}/;
    if (offercode == "") {
        document.getElementById("offercode_error").innerHTML = "*This field is required";
        return false;
    }
    if (offercode != "") {
        document.getElementById("offercode_error").innerHTML = "";
    }

    if (discount == "") {
        document.getElementById("discount_error").innerHTML = "*This field is required";
        return false;
    }
    if (discount != "") {
        document.getElementById("discount_error").innerHTML = "";
    }

    if (minAmount == "") {
        document.getElementById("minamount_error").innerHTML = "*This field is required";
        return false;
    }
    if (minAmount != "") {
        document.getElementById("minamount_error").innerHTML = "";
    }

    if (expDate == "") {
        document.getElementById("expdate_error").innerHTML = "*This field is required";
        return false;
    }
    if (expDate != "") {
        document.getElementById("expdate_error").innerHTML = "";
    }


    if(!offer_pattern.test(offercode)){
        document.getElementById("offercode_error").innerHTML="*Offer Code is should be 4-6 character";
        return false;
    }
    else {
        return true;
    }
}






