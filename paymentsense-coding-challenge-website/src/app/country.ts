import { logging } from "protractor";

export interface Country {
    name: string;

    alpha2Code: string;

    capital: string;

    population: bigint;

    numericCode: number;

    currencies: Currency[];

    languages: Language[];

    flag: string;

}

interface Currency {
    code: string;

    name: string;

    symbol: string;
}

interface Language {
    name: string;
}