export class Task {
  id: string;
  name: string;
  description: string;
  failIfAnyElementHasError: boolean;
  failIfAnyElementHasWarning: boolean;
  position: number;
  isEnabled: boolean;
  parentJobId: string;
  creationDate: Date;
  lastUpdateDate: Date;
}
