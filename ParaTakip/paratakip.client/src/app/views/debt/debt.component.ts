import { Component, OnInit } from '@angular/core';
import { CommonProviders } from 'src/app/helpers/commonProviders';
import { ToastService } from 'src/app/services/toast.service';
import { AlertComponent } from '@coreui/angular';
import { CreditCardDirectivesModule } from 'angular-cc-library';
import { CommonModule } from '@angular/common';
import { IgxMaskModule, IgxInputGroupModule, IgxIconModule } from 'igniteui-angular';
import { Debt, DebtInfo } from 'src/app/models/entities/debt';
import { DebtService } from 'src/app/services/debt-service';

@Component({
  selector: 'app-debt',
  templateUrl: 'debt.component.html',
  styleUrls: ['debt.component.scss'],
  standalone: true,
  imports: [CommonProviders.Set(), AlertComponent, CreditCardDirectivesModule, CommonModule, IgxMaskModule,
    IgxInputGroupModule,
    IgxIconModule],
})

export class DebtComponent implements OnInit {

  public localCurrency = 'TRY';
  public Intl = Intl;
  public debt: Debt = new Debt();

  bankList = [
    'Akbank',
    'Aktif Yatırım Bankası',
    'Albaraka Türk',
    'Alternatif Bank',
    'Anadolubank',
    'Arap Türk Bankası',
    'Burgan Bank',
    'Citibank',
    'DenizBank',
    'Deutsche Bank',
    'Fibabanka',
    'Garanti BBVA',
    'HSBC',
    'Halkbank',
    'ICBC Turkey Bank',
    'ING',
    'Kuveyt Türk',
    'Odeabank',
    'PASHA Bank',
    'QNB Finansbank',
    'TEB',
    'Türk Eximbank',
    'Türk Ticaret Bankası',
    'Türkiye Emlak Katılım Bankası',
    'Türkiye Finans Katılım Bankası',
    'Türkiye Kalkınma Bankası',
    'Türkiye Sınai Kalkınma Bankası',
    'Türkiye İş Bankası',
    'Vakıf Katılım Bankası',
    'VakıfBank',
    'Yapı Kredi',
    'Ziraat Bankası',
    'Ziraat Katılım',
    'İller Bankası',
    'İstanbul Takasbank',
    'Şekerbank',
    'Kiptaş'
  ]

  debtListHeaders = ['Borç Tipi', 'Banka', 'Vade Tarihi', 'Toplam Borç', 'Taksit Sayısı', 'Aylık Taksit Tutarı', '']

  form: DebtInfo = new DebtInfo();
  constructor(public debtService: DebtService, public toastService: ToastService) { }

  ngOnInit() {
    this.debtService.get().subscribe({
      next: (v) => {
        this.debt = v;
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })
  }

  getCurrencyFormat(value:number){
    return new Intl.NumberFormat('tr-TR', { style: 'currency', currency: 'TRY' }).format(value);
  }

  saveDebt() {

    this.debt.values.push(this.form);
    this.debtService.update(this.debt.values).subscribe({
      next: (v) => {
        this.debt = v;
        this.form = new DebtInfo();
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })
  }

  delete(debtInfo: DebtInfo) {

  }

  getDebtSum(){
    return this.debt.values.reduce((a, b) => a + b.totalAmount, 0);
  }

  getDebtTypeDesc(debtType: number) {
    switch (debtType) {
      case -1:
        return 'Seçilmedi';
      case 0:
        return 'Kredi';
      default:
        return '';
    }
  }
}
