﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Esoft.Classes.DataSqlGateways;
using Esoft.Classes.Models.Apartment;
using Esoft.Classes.Models.Complex;
using Esoft.Classes.Models.House;

namespace Esoft.Classes.DataAdapters
{
    public class ComplexAdapter
    {
        public ComplexSqlGateway ComplexAccess { get; }
        public HouseSqlGateway HouseAccess { get; }


        public ComplexAdapter()
        {
            ComplexAccess = new ComplexSqlGateway();
            HouseAccess = new HouseSqlGateway();
        }

        public List<Complex> GetAllComplex()
        {
            var complexList = ComplexAccess.SelectAllComplex();
            return complexList ?? new List<Complex>();
        }

        public List<ComplexWithHouses> GetAllComplexWithHouses()
        {
            var complexList = ComplexAccess.SelectAllComplexWithHouses();
            return complexList ?? new List<ComplexWithHouses>();
        }
        public List<ComplexWithHouses> GetAllComplexWithHousesSorted()
        {
            var complexListSorted = ComplexAccess.SelectAllComplexWithHouses()
                .OrderBy(s => s.City)
                .ThenBy(s => s.StatusConstructionHousingComplexName)
                .ToList();

            var HouseList = new ObservableCollection<House>(HouseAccess.SelectAllHouse());

            foreach (var complex in complexListSorted)
                complex.HouseCount = HouseList.Count(x => x.IdComplex.Equals(complex.IdComplex));

            return complexListSorted ?? new List<ComplexWithHouses>();
        }

        public bool IsDeleteAvailable(Complex newComplex)
        {
            return ComplexAccess != null && ComplexAccess.IsDeleteComplexPossible(newComplex);
        }
        public bool IsPlanAvailable(Complex newComplex)
        {
            return ComplexAccess != null && ComplexAccess.IsDeleteComplexPossible(newComplex);
        }


        #region CRUD operations for Complex 


        public bool AddComplex(Complex newComplex)
        {
            return ComplexAccess != null && ComplexAccess.InsertComplex(newComplex);
        }

        public Complex GetComplex(Complex selectComplex)
        {
            if (ComplexAccess != null) return ComplexAccess.SelectComplex(selectComplex);
            return new Complex();
        }

        public ComplexWithHouses GetComplexWithHouses(Complex selectComplex)
        {
            if (ComplexAccess != null)
            {
                var temp = ComplexAccess.SelectComplex(selectComplex);
                var downCastedComplex = new ComplexWithHouses
                {
                    AddedValue = temp.AddedValue,
                    BuildingCost = temp.BuildingCost,
                    City = temp.City,
                    IdComplex = temp.IdComplex,
                    IsDeleted = temp.IsDeleted,
                    StatusConstructionHousingComplex = temp.StatusConstructionHousingComplex,
                    StatusConstructionHousingComplexName = temp.StatusConstructionHousingComplex,
                    NameHousingComplex = temp.NameHousingComplex
                };
                
                return downCastedComplex;
            }
                
            return new ComplexWithHouses();
        }

        public bool SetComplex(Complex updateComplex)
        {
            return ComplexAccess != null && ComplexAccess.UpdateComplex(updateComplex);
        }

        public bool DeleteComplex(Complex deleteComplex)
        {
            return deleteComplex != null 
                   && HouseAccess != null
                   && ComplexAccess != null
                   && HouseAccess.DeleteHouseByComplexId(deleteComplex.IdComplex)
                   && ComplexAccess.DeleteComplex(deleteComplex);
        }

        #endregion

    }
}
