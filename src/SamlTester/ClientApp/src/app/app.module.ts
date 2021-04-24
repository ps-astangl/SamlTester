import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AssertionBoxComponent } from './assertion-box/assertion-box.component';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { HttpClientModule } from '@angular/common/http';
import {SafePipe} from './assertion-box/safe.pipe';
import { LaunchSamlWindowComponent } from './launch-saml-window/launch-saml-window.component';

@NgModule({
  declarations: [
    AppComponent,
    AssertionBoxComponent,
    SafePipe,
    LaunchSamlWindowComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    MatSelectModule,
    HttpClientModule,
  ],
  providers: [
    SafePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
