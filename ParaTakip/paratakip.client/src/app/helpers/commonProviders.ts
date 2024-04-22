import { DatePipe, NgFor, NgIf } from "@angular/common";
import { FormControl, FormsModule, NgControl, NgModel, ReactiveFormsModule } from "@angular/forms";
import { CardComponent, CardHeaderComponent, CardBodyComponent, CardFooterComponent, RowComponent, ColComponent, TableDirective, ButtonGroupComponent, ButtonDirective, FormCheckLabelDirective, ProgressBarDirective, ProgressComponent, TextColorDirective, GutterDirective, AvatarComponent, ModalComponent, ModalHeaderComponent, ModalBodyComponent, ModalFooterComponent, ModalToggleDirective, FormControlDirective, ButtonCloseDirective, FormSelectDirective, InputGroupComponent, PopoverComponent, PopoverDirective, PopoverModule } from "@coreui/angular";
import { ChartjsComponent } from "@coreui/angular-chartjs";
import { IconDirective } from "@coreui/icons-angular";
import { WidgetsBrandComponent } from "../views/widgets/widgets-brand/widgets-brand.component";
import { WidgetsDropdownComponent } from "../views/widgets/widgets-dropdown/widgets-dropdown.component";
import { HttpService } from "../services/http.service";

export class CommonProviders {

    constructor() { }

    static Set() {
        return [
            FormsModule,
            NgFor,
            NgIf,
            CardComponent,
            CardHeaderComponent,
            CardBodyComponent,
            CardFooterComponent,
            RowComponent,
            ColComponent,
            TableDirective,
            ButtonGroupComponent,
            ButtonDirective,
            FormCheckLabelDirective,
            ProgressBarDirective,
            ProgressComponent,
            TextColorDirective,
            GutterDirective,
            IconDirective,
            ReactiveFormsModule,
            ModalComponent,
            ModalHeaderComponent,
            ModalBodyComponent,
            ModalFooterComponent,
            ModalToggleDirective,
            FormControlDirective,
            ButtonCloseDirective,
            FormSelectDirective,
            DatePipe,
            InputGroupComponent,
            PopoverComponent, PopoverDirective
        ];
    }
}