using Microsoft.AspNetCore.Mvc;

namespace WaterMonitorSimple.Endpoints;

public static class ExcelEndpoints {
	public static void AddExcelEndpoints(this RouteGroupBuilder routeGroupBuilder) {
		var excelGroup = routeGroupBuilder.MapGroup("/excel")
			.WithOpenApi()
			.WithTags("Excel")
			.WithDescription("Endpoints for interacting with Excel documents");

		excelGroup.MapGet("/download", async (
			//[FromServices] 
		) => {

		});	
	}
}