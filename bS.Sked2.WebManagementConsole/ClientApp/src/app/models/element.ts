export class ElementDetail {
  id: string;
  name: string;
  description: string;
  type: ElementType;
  parentModuleId: string;
  parentTaskId: string;
  inputProperties: ElementProperty[];
  outputProperties: ElementProperty[];
}

export class ElementProperty {
  key: string;
  value: string;
  description: string;
  type: PropertyType;
}

export class ElementType {
  key: string;
  name: string;
  description: string;
}

export class PropertyType {
  key: string;
  name: string;
}

