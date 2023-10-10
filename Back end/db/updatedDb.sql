create database LMS3;

use LMS3;

create table categories(
category varchar(100) primary key,
);

create table materials(
material varchar(100) primary key,
);

create table category_material
(
    category varchar(100),
    material varchar(100),
    PRIMARY KEY (category, material),
    FOREIGN KEY (category) REFERENCES categories(category) ON DELETE CASCADE,
    FOREIGN KEY (material) REFERENCES materials(material) ON DELETE CASCADE

);

create table item_master (
item_id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
issue_status VARCHAR(8) CHECK (issue_status IN ('issued', 'waiting', 'rejected')),
item_description varchar(25),
item_make varchar(100) references materials(material) ON DELETE set null,
item_category varchar(100) references categories(category) ON DELETE set null,
item_valuation int
);

create table employee_master(
employee_id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
employee_name varchar(20) not null,
designation varchar(20),
department varchar(25),
gender VARCHAR(30) CHECK (gender IN ('Male', 'Female','Prefer not to Say')),
date_of_birth date,
date_of_joining date default cast(getdate() as date)
);

create table employee_credentials(
employee_id UNIQUEIDENTIFIER references employee_master(employee_id),
employee_email varchar(100) primary key,
employee_password varchar(100) not null,
employee_role VARCHAR(10) CHECK (employee_role IN ('Admin', 'Employee')),
);

create table employee_issue_details(
issue_id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
employee_id UNIQUEIDENTIFIER references employee_master(employee_id) on delete cascade,
item_id UNIQUEIDENTIFIER references item_master(item_id) on delete cascade,
issue_date date default cast(getdate() as date),
return_date date
);

create table loan_card_master(
loan_id UNIQUEIDENTIFIER DEFAULT NEWID() primary key,
loan_type varchar(100) references categories(category) on delete cascade,
duration_in_years int,
);


create table employee_card_details(
employee_id UNIQUEIDENTIFIER references employee_master(employee_id) on delete cascade,
loan_id UNIQUEIDENTIFIER references loan_card_master(loan_id) on delete cascade,
card_issue_date date default cast(getdate() as date),
PRIMARY KEY (employee_id, loan_id)
);

create table loan_request(
employee_id UNIQUEIDENTIFIER references employee_master(employee_id) on delete cascade,
item_id UNIQUEIDENTIFIER references item_master(item_id) on delete cascade,
loan_id UNIQUEIDENTIFIER references loan_card_master(loan_id) on delete cascade,
PRIMARY KEY (employee_id,item_id,loan_id)
);

ALTER TABLE loan_card_master
ADD CONSTRAINT UC_LoanType_Duration UNIQUE (loan_type, duration_in_years);

ALTER TABLE loan_request
ADD request_id UNIQUEIDENTIFIER DEFAULT NEWID();

ALTER TABLE loan_request
ALTER COLUMN request_id UNIQUEIDENTIFIER NOT NULL;

ALTER TABLE loan_request
ADD CONSTRAINT UC_EmployeeItem UNIQUE (employee_id, item_id);

ALTER TABLE loan_request
DROP CONSTRAINT PK__loan_request;

ALTER TABLE loan_request
ADD CONSTRAINT PK_loan_request PRIMARY KEY (request_id);

ALTER TABLE employee_card_details
ADD employee_card_id UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL;

ALTER TABLE employee_card_details
DROP CONSTRAINT PK__employee__9F3172FD1203A131;

ALTER TABLE employee_card_details
ADD CONSTRAINT PK_EMPLOYEE_CARD_DETAILS PRIMARY KEY(employee_card_id);



