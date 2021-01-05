$(document).ready(function(){

	$("#addbtn").click(function(){
		addCategory();
	});

	var loadCategories=function(){
			$.ajax({
			url:"http://localhost:56953/api/categories",
			method:"GET",
			complete:function(xmlhttp,status){
				if(xmlhttp.status==200)
				{
					var data=xmlhttp.responseJSON;
					//$("#msg").html(data[0]);
					//console.log(data[0]);

					var str='';
					for (var i = 0; i < data.length; i++) {
						str+="<tr><td>"+data[i].categoryId+"</td><td>"+data[i].categoryName+"</td></tr>";
					}

					$("#categoryList tbody").html(str);
				}
				else
				{
					$("#msg").html(xmlhttp.status+":"+xmlhttp.statusText);
				}
		}
		});
	}

	loadCategories();

	var addCategory=function(){
			$.ajax({
			url:"http://localhost:56953/api/categories",
			method:"POST",
			header:"Content-Type:application/json",
			data:{
				categoryName:$("#addCategoryName").val()
			},
			complete:function(xmlhttp,status){
				if(xmlhttp.status==201)
				{
					
					$("#msg").html("Category created");
					loadCategories();
				}
				else
				{
					$("#msg").html(xmlhttp.status+":"+xmlhttp.statusText);
				}
			}
		});
		}
});

