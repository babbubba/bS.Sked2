export class JobDetail {
    id: string;
    name: string;
    description: string;
    failIfAnyTaskHasError: boolean;
    failIfAnyTaskHasWarning: boolean;
    isEnabled: boolean;
    creationDate: Date;
    lastUpdateDate: Date;
}
