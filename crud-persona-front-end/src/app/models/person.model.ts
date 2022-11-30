export interface ContactMeans {
    contactMeansId: number;
    description: string;
}

export interface ContactMeansPeople {
    id: number;
    contactMeansId: number;
    personId: number;
    contact: string;
    contactMeans?: ContactMeans;
}

export interface Person {
    personId: number;
    firstName: string;
    lastName: string;
    identification: string;
    birth: Date;
    contactMeansPeople: ContactMeansPeople[];
}