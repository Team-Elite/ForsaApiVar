use forsa
select * from tbluser

USP_VerifyEmailAddress 'ishu@cityinfomart.com'

select * from sys.objects order by modify_date desc

sp_helptext USP_BestPriceView_GetBanksByTimePeriod

select * from tbluser where userid=4

update tbluser set EmailAddress='2011varinder@gmail.com' where userId=4

-- USP_BestPriceView_GetBanksByTimePeriod 4,2,1
alter Procedure USP_BestPriceView_GetBanksByTimePeriod
@UserId int,
@TimePeriodId int,
@PageNumber int=0
as

declare @NumberOfRecordsPerPage int=1
-- Getting client group Id
declare @ClientGroupId int =(select ClientGroupId from tblUser where userId =@UserId)

-- Getting userIds on the basis of client group after filtering records from groupId field in tblUser colulmn
declare @UserIds nvarchar(4000)=''
select @UserIds= Coalesce(@UserIds+',','')+cast (userId as nvarchar) from tbluser where Len(RTrim(Ltrim(groupIds))) !=0 and groupIds is not null and (select top 1 items from dbo.fn_split(GroupIds,',') 
where items=@ClientGroupId order by 1 desc) =@ClientGroupId
--select @UserIds

select * from (
select row_Number() over(order by RateOfInterest desc) SrNo, TU.UserId,TU.Bank, cast(TROIOB.RateOfInterest as nvarchar) RateOfInterest from tblRateOfInterestOfBank TROIOB 
inner join tblUser TU on TU.UserId =TROIOB.UserId
where TimePeriodid=@TimePeriodId
and TROIOB.UserId in (select Items from fn_split(@UserIds,',')) and IsPublished=1) as t
--where SrNo between @NumberOfRecordsPerPage*(@PageNumber-1)+1 and @NumberOfRecordsPerPage * @PageNumber
order by RateOfInterest desc
