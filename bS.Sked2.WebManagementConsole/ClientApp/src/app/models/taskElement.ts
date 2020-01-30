export class TaskElement {
  id: string;
  name: string;
  description: string;
  type: ElementType;
  parentModuleId: string;
  parentTaskId: string;
  inputProperties: ElementProperty[];
  outputProperties: ElementProperty[];
  previousId: string;
  nextId: string;
  position: number;
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

