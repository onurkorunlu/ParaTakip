import { Component } from '@angular/core';
import { NgStyle } from '@angular/common';
import { IconDirective } from '@coreui/icons-angular';
import { ContainerComponent, RowComponent, ColComponent, CardGroupComponent, TextColorDirective, CardComponent, CardBodyComponent, FormDirective, InputGroupComponent, InputGroupTextDirective, FormControlDirective, ButtonDirective } from '@coreui/angular';
import { FormsModule } from '@angular/forms';
import { LoginModel } from 'src/app/models/requestModel/loginModel';
import { LoginResultModel } from 'src/app/models/responseModel/loginResultModel';
import { UserService } from 'src/app/services/user.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { ToastService } from 'src/app/services/toast.service';
import { Router, RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { animate } from '@angular/animations';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [RouterModule ,FormsModule, ContainerComponent, RowComponent, ColComponent, CardGroupComponent, TextColorDirective, CardComponent, CardBodyComponent, FormDirective, InputGroupComponent, InputGroupTextDirective, IconDirective, FormControlDirective, ButtonDirective, NgStyle,

  ]
})
export class LoginComponent {

  loginModel: LoginModel = <LoginModel>{
    username: "testuser",
    password: "t123456789"
  };

  constructor(private userService: UserService, private tokenStorageService: TokenStorageService, private toastService: ToastService, private router: Router) { }

  public login() {

    this.userService.login(this.loginModel).subscribe({
      next: (result: LoginResultModel) => {
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
        if (err.errors) {
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
