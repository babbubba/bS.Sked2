import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpEvent, HttpHandler, HttpRequest, HttpInterceptor } from '@angular/common/http';
//import { Router } from '@angular/router';
export class ErrorIntercept implements HttpInterceptor {

  //constructor(private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        console.log(error);
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
          // client-side error
          errorMessage = `Error: ${error.error.message}`;
        }
        else {
          errorMessage = `Error Status: ${error.status}\nMessage: ${error.message}`;

          // server-side error
          switch (error.status) {
            case 401: //login
              //this.router.navigateByUrl("/login");
              break;
            case 403: //forbidden
              errorMessage = `You are not authorized to this resource. Please contact your administrator.`;
              break;
            case 500: //server error
              errorMessage = `Server cannot respond to this request. Please try again later.`;
          }

        }
        console.error(errorMessage);
        return throwError(errorMessage);
      })
    )
  }
}
