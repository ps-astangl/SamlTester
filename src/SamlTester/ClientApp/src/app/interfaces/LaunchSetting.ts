
export interface ISamlLaunchConfiguration {
  issuer: string,
  attributes: ISamlAttribute[]
}

export interface ISamlAttribute {
  name: string,
  value: string
}

export class SamlAttribute implements ISamlAttribute {
  constructor(name: string , value: string) {
    this.name = name;
    this.value = value;
  }
  value: string;
  name: string;
}

export class SamlLaunchConfiguration implements ISamlLaunchConfiguration {
  constructor(attributes: SamlAttribute[], issuer?: string) {
    this.attributes = this._setAttributes(attributes);
    this.issuer = this._setIssuer(issuer);
  }

  _setIssuer(issuer: string | null | undefined) {
    if (issuer === null || issuer === undefined) {
      return '';
    }
    else {
      this.issuer = issuer;
      return this.issuer;
    }
  }

  _setAttributes(attributes: SamlAttribute[] | null | undefined) {
    if (attributes === null || attributes === undefined) {
      return [];
    }
    else {
      this.attributes = attributes;
      return this.attributes;
    }
  }

  attributes: SamlAttribute[];
  issuer: string;
}
