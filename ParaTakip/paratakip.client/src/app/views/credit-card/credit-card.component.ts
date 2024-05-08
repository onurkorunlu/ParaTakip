import { Component, OnInit } from '@angular/core';
import { CommonProviders } from 'src/app/helpers/commonProviders';
import { ExchangeRateService } from 'src/app/services/exchange-rate-service';
import { WealthService } from 'src/app/services/wealth-service';
import { ToastService } from 'src/app/services/toast.service';
import { FundService } from 'src/app/services/fund-service';
import { StockService } from 'src/app/services/stock-service';
import { AlertComponent } from '@coreui/angular';
import { CreditCardDirectivesModule } from 'angular-cc-library';
import { CommonModule } from '@angular/common';
import { IgxMaskModule, IgxInputGroupModule, IgxIconModule } from 'igniteui-angular';
import { CreditCard } from 'src/app/models/entities/creditCard';
import { CreditCardService } from 'src/app/services/credit-card.service';

@Component({
  selector: 'app-credit-card',
  templateUrl: 'credit-card.component.html',
  styleUrls: ['credit-card.component.scss'],
  standalone: true,
  imports: [CommonProviders.Set(), AlertComponent, CreditCardDirectivesModule, CommonModule, IgxMaskModule,
    IgxInputGroupModule,
    IgxIconModule],
})

export class CreditCardComponent implements OnInit {

  cardListHeaders: string[] = ['Kart Brand','Banka', 'Maskeli Kart No', 'Ekstre Günü', 'Son Ödeme Günü', ''];
  cardList: CreditCard[] = [
    <CreditCard>{ maskedCardNumber: '5502 87XX XXXX 3456', statementDay: 1, lastPaymentDay: 1 },
    <CreditCard>{ maskedCardNumber: '4010 87XX XXXX 3456', statementDay: 1, lastPaymentDay: 1 },
    <CreditCard>{ maskedCardNumber: '3450 87XX XXXX 3456', statementDay: 1, lastPaymentDay: 1 },
    <CreditCard>{ maskedCardNumber: '9792 87XX XXXX 3456', statementDay: 1, lastPaymentDay: 1 }
  ];

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
  ]

  public Intl = Intl;
  public form: CreditCard = new CreditCard();
  public defaultMask: string = '9999 9999 9999 9999'
  constructor(private creditCardService: CreditCardService, public toastService: ToastService) { }

  ngOnInit() {
    this.creditCardService.get().subscribe({
      next: (v) => {
        this.cardList = v;
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })
  }

  getCreditCardType(creditCardNumber: string) {
    // start without knowing the credit card type
    var result = "unknown";

    // first check for MasterCard
    if (/^5[1-5]/.test(creditCardNumber)) {
      result = "mastercard";
    }
    // then check for Visa
    else if (/^4/.test(creditCardNumber)) {
      result = "visa";
    }
    else if (/^9792|^65|^3657|^2205/.test(creditCardNumber)) {
      result = "troy";
    }
    // then check for AmEx
    else if (/^3[47]/.test(creditCardNumber)) {
      result = "amex";
    }
    // then check for Discover
    else if (/6(?:011|5[0-9]{2})[0-9]{12}/.test(creditCardNumber)) {
      result = "discover";
    }

    return result;
  }


  saveCreditCard() {
    this.creditCardService.add(this.form).subscribe({
      next: (v) => {
        this.cardList.push(v);
        this.form = new CreditCard();
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })
  }

  delete(card: CreditCard) {
    this.creditCardService.delete(card.stringRecordId).subscribe({
      next: (v) => {
        this.cardList = this.cardList.filter(x => x.stringRecordId != card.stringRecordId);
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })
  }
}
