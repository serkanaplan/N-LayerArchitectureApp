using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters;

public class ValidateFilterAttribute : ActionFilterAttribute
{
    // action metoda girmeden önce vallidasyondan geçmiş mi geçmemiş mi kontrol ediyoruz eğer validasyondan geçmediyse action metoda girmez bile. Validasyondan geçmediyse kaydedilmeyecek zaten oyuzden action metoda girmesine gerek yok 
    // filter yazmazsan action metoda grer ve validasyondan geçmezse 
    public override void OnActionExecuting(ActionExecutingContext context)
    {

        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            // validasyondan geçmezse 400 durum koduyla beraber uyarıları mesajlarını döner
            context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));
        }
    }
}