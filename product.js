$(document).ready(function(){

	$("#addbtn").click(function(){
		addProduct();
	});

	var loadProducts=function(){
			$.ajax({
			url:"http://localhost:56953/api/products",
			method:"GET",
			complete:function(xmlhttp,status){
				if(xmlhttp.status==200)
				{
					var data=xmlhttp.responseJSON;
					//$("#msg").html(data[0]);
					//console.log(data[0]);

					var str='';
					for (var i = 0; i < data.length; i++) {
						str+="<tr><td>"+data[i].productId+"</td><td>"+data[i].productName+"</td></tr>";
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

	loadProducts();

	var addProduct=function(){
			$.ajax({
			url:"http://localhost:56953/api/products",
			method:"POST",
			header:"Content-Type:application/json",
			data:{
				productName:$("#addItemName").val()
			},
			complete:function(xmlhttp,status){
				if(xmlhttp.status==201)
				{
					
					$("#msg").html("Item created");
					loadProducts();
				}
				else
				{
					$("#msg").html(xmlhttp.status+":"+xmlhttp.statusText);
				}
			}
		});
		}
});

