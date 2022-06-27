import { Country } from '../country';

export const MOCKCOUNTRIES: Country[] = [
  {
    name: 'Afghanistan',
    alpha2Code: 'AF',
    capital: 'Kabul',
    population: 40218234 as unknown as bigint,
    numericCode: 4,
    currencies: [
      {
        code: 'AFN',
        name: 'Afghan afghani',
        symbol: '؋'
      }
    ],
    languages: [
      {
        name: 'Pashto'
      },
      {
        name: 'Uzbek'
      },
      {
        name: 'Turkmen'
      }
    ],
    flag: 'https://upload.wikimedia.org/wikipedia/commons/5/5c/Flag_of_the_Taliban.svg'
  },
  {
    name: 'Åland Islands',
    alpha2Code: 'AX',
    capital: 'Mariehamn',
    population: 28875 as unknown as bigint,
    numericCode: 248,
    currencies: [
      {
        code: 'EUR',
        name: 'Euro',
        symbol: '€'
      }
    ],
    languages: [
      {
        name: 'Swedish'
      }
    ],
    flag: 'https://flagcdn.com/ax.svg'
  },
  {
    name: 'Albania',
    alpha2Code: 'AL',
    capital: 'Tirana',
    population: 2837743 as unknown as bigint,
    numericCode: 8,
    currencies: [
      {
        code: 'ALL',
        name: 'Albanian lek',
        symbol: 'L'
      }
    ],
    languages: [
      {
        name: 'Albanian'
      }
    ],
    flag: 'https://flagcdn.com/al.svg'
  },
  {
    name: 'Algeria',
    alpha2Code: 'DZ',
    capital: 'Algiers',
    population: 44700000 as unknown as bigint,
    numericCode: 12,
    currencies: [
      {
        code: 'DZD',
        name: 'Algerian dinar',
        symbol: 'د.ج'
      }
    ],
    languages: [
      {
        name: 'Arabic'
      }
    ],
    flag: 'https://flagcdn.com/dz.svg'
  },
  {
    name: 'American Samoa',
    alpha2Code: 'AS',
    capital: 'Pago Pago',
    population: 55197 as unknown as bigint,
    numericCode: 16,
    currencies: [
      {
        code: 'USD',
        name: 'United States Dollar',
        symbol: '$'
      }
    ],
    languages: [
      {
        name: 'English'
      },
      {
        name: 'Samoan'
      }
    ],
    flag: 'https://flagcdn.com/as.svg'
  }
];
