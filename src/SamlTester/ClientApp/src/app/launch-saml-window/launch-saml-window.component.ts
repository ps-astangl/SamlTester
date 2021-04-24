import {Component, Input, OnInit} from '@angular/core';
import {SafePipe} from "../assertion-box/safe.pipe";

@Component({
  selector: 'app-launch-saml-window',
  templateUrl: './launch-saml-window.component.html',
  styleUrls: ['./launch-saml-window.component.css']
})
export class LaunchSamlWindowComponent implements OnInit {
  @Input() samlLaunchHtml: any;

  constructor( private safePipe: SafePipe) { }

  SanitizeText(text:string) {
    return this.safePipe.transform(text, 'html')
  }

  RenderHtml(html:string) {
    return this.samlLaunchHtml = this.SanitizeText(html);
  }

  ngOnInit() {
  }

}
