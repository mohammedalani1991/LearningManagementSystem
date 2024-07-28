
if not exists (select Id from DetailsLookupTranslation where [Value]=N'ذكور')
BEGIN

  INSERT into DetailsLookupTranslation ([DetailsLookupId], [Value], [LanguageId], [IsDefault])
 values ((select Id from DetailsLookup where Code='Males'),N'ذكور',7,0)



  INSERT into DetailsLookupTranslation ([DetailsLookupId], [Value], [LanguageId], [IsDefault])
 values ((select Id from DetailsLookup where Code='Females'),N'إناث',7,0)


   INSERT into DetailsLookupTranslation ([DetailsLookupId], [Value], [LanguageId], [IsDefault])
 values ((select Id from DetailsLookup where Code='Mixed'),N'مختلط',7,0)
end

