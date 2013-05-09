function validateForm()
{
	var x=document.forms["personData"]["FirstName"].value;
	if (x==null || x=="")
	{
		alert("First name must be filled out");
		return false;
	}
	
	x=document.forms["personData"]["LastName"].value;
	if (x==null || x=="")
	{
		alert("Last name must be filled out");
		return false;
	}

	x=document.forms["personData"]["Sex"].value;
	if (x==null || x=="")
	{
		alert("Sex must be chosen");
		return false;
	}

	x=document.forms["personData"]["Country"].value;
	if (x==null || x=="")
	{
		alert("Country must be filled out");
		return false;
	}

	x=document.forms["personData"]["City"].value;
	if (x==null || x=="")
	{
		alert("City must be filled out");
		return false;
	}

	x=document.forms["personData"]["Language"].value;
	if (x==null || x=="")
	{
		alert("Language must be chosen");
		return false;
	}

	x=document.forms["personData"]["UserName"].value;
	if (x==null || x=="")
	{
		alert("User name must be filled out");
		return false;
	}

	x=document.forms["personData"]["Password"].value;
	if (x==null || x=="")
	{
		alert("Password must be filled out");
		return false;
	}

	x=document.forms["personData"]["Email"].value;
	if (x==null || x=="")
	{
		alert("E-mail must be filled out");
		return false;
	}

	x=document.forms["personData"]["Photo"].value;
	if (x==null || x=="")
	{
		alert("Photo must be chosen");
		return false;
	}
	
	x=document.forms["personData"]["Description"].value;
	if (x==null || x=="")
	{
		alert("Description must be filled out");
		return false;
	}
}

function forgotPassword()
{
	var y=document.forms["Email"]["Email"].value;
	if (y==null || y=="")
	{
		alert("E-mail must be filled out");
		return false;
	}
}