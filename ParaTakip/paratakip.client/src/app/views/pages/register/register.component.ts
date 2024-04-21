import { Component } from '@angular/core';
import { IconDirective } from '@coreui/icons-angular';
import { ContainerComponent, RowComponent, ColComponent, TextColorDirective, CardComponent, CardBodyComponent, FormDirective, InputGroupComponent, InputGroupTextDirective, FormControlDirective, ButtonDirective } from '@coreui/angular';
import { RegisterModel } from 'src/app/models/requestModel/registerModel';
import { UserService } from 'src/app/services/user.service';
import { RegisterResultModel } from 'src/app/models/responseModel/registerResultModel';
import { Router, RouterModule } from '@angular/router';
import { ToastService } from 'src/app/services/toast.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
    standalone: true,
    imports: [RouterModule,FormsModule,ContainerComponent, RowComponent, ColComponent, TextColorDirective, CardComponent, CardBodyComponent, FormDirective, InputGroupComponent, InputGroupTextDirective, IconDirective, FormControlDirective, ButtonDirective]
})
export class RegisterComponent {

  registerModel: RegisterModel = <RegisterModel>{
    username: "",
    emailAddress: "",
    password: "",
    password2: ""
  };

  constructor(private userService:UserService,private tokenStorageService: TokenStorageService, private toastService: ToastService, private router: Router) { }

  public register() {

    this.userService.register(this.registerModel).subscribe({
      next: (result: RegisterResultModel) => {
        console.log(result);
        if (result.isSuccess) {
          this.tokenStorageService.saveToken(result.token);
          this.tokenStorageService.saveUser(result.appUser);
          this.tokenStorageService.saveRole(result.appUserRole);
          this.toastService.showSuccess('Giriş Başarılı');
          this.router.navigate([""]);
        }
        else {
          this.toastService.showError(result.message);
        }
      },
      complete: () => { },
      error: (err) => {
        if(err.errors){
          Object.values(err.errors).forEach((e: any) => {
            e.forEach((e: any) => {
              this.toastService.showError(e);
            });
          });
        }
      }
    });
  }
}
