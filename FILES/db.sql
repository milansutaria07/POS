--
-- PostgreSQL database dump
--


-- Dumped from database version 18.1
-- Dumped by pg_dump version 18.0

-- Started on 2026-08-29 12:51:24

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 6 (class 2615 OID 45487)
-- Name: login; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA login;


ALTER SCHEMA login OWNER TO postgres;

--
-- TOC entry 222 (class 1255 OID 45505)
-- Name: CreateUser(text, text, text); Type: FUNCTION; Schema: login; Owner: postgres
--

CREATE FUNCTION login."CreateUser"("_Username" text, "_Password" text, "_Usertype" text) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN

    INSERT INTO login."Users"
    (
        "UserName",
        "Password",
        "UserType"
    )
    VALUES
    (
        "_Username",
        "_Password",
        "_Usertype"
    );

END;
$$;


ALTER FUNCTION login."CreateUser"("_Username" text, "_Password" text, "_Usertype" text) OWNER TO postgres;

--
-- TOC entry 223 (class 1255 OID 45507)
-- Name: ValidateUser(text, text); Type: FUNCTION; Schema: login; Owner: postgres
--

CREATE FUNCTION login."ValidateUser"("_Username" text, "_Password" text) RETURNS TABLE("IsValid" boolean, "UserType" text)
    LANGUAGE plpgsql
    AS $$
BEGIN

    RETURN QUERY
    SELECT
        TRUE AS "IsValid",
        u."UserType"
    FROM login."Users" u
    WHERE u."UserName" = "_Username"
      AND u."Password" = "_Password";

    IF NOT FOUND THEN
        RETURN QUERY
        SELECT
            FALSE AS "IsValid",
            NULL::TEXT AS "UserType";
    END IF;

END;
$$;


ALTER FUNCTION login."ValidateUser"("_Username" text, "_Password" text) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 221 (class 1259 OID 45489)
-- Name: Users; Type: TABLE; Schema: login; Owner: postgres
--

CREATE TABLE login."Users" (
    "Id" integer NOT NULL,
    "UserName" text NOT NULL,
    "Password" text NOT NULL,
    "UserType" text NOT NULL,
    "Stamp" timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE login."Users" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 45488)
-- Name: Users_Id_seq; Type: SEQUENCE; Schema: login; Owner: postgres
--

CREATE SEQUENCE login."Users_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE login."Users_Id_seq" OWNER TO postgres;

--
-- TOC entry 5018 (class 0 OID 0)
-- Dependencies: 220
-- Name: Users_Id_seq; Type: SEQUENCE OWNED BY; Schema: login; Owner: postgres
--

ALTER SEQUENCE login."Users_Id_seq" OWNED BY login."Users"."Id";


--
-- TOC entry 4859 (class 2604 OID 45492)
-- Name: Users Id; Type: DEFAULT; Schema: login; Owner: postgres
--

ALTER TABLE ONLY login."Users" ALTER COLUMN "Id" SET DEFAULT nextval('login."Users_Id_seq"'::regclass);


--
-- TOC entry 5012 (class 0 OID 45489)
-- Dependencies: 221
-- Data for Name: Users; Type: TABLE DATA; Schema: login; Owner: postgres
--

INSERT INTO login."Users" ("Id", "UserName", "Password", "UserType", "Stamp") VALUES (1, 'admin', 'Admin@123', 'Administrator', '2026-08-17 00:36:24.86395');
INSERT INTO login."Users" ("Id", "UserName", "Password", "UserType", "Stamp") VALUES (3, 'admin2', 'Admin@123', 'Normal', '2026-08-17 00:36:24.86395');


--
-- TOC entry 5019 (class 0 OID 0)
-- Dependencies: 220
-- Name: Users_Id_seq; Type: SEQUENCE SET; Schema: login; Owner: postgres
--

SELECT pg_catalog.setval('login."Users_Id_seq"', 3, true);


--
-- TOC entry 4863 (class 2606 OID 45502)
-- Name: Users Users_pkey; Type: CONSTRAINT; Schema: login; Owner: postgres
--

ALTER TABLE ONLY login."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4861 (class 1259 OID 45503)
-- Name: UX_Users_UserName; Type: INDEX; Schema: login; Owner: postgres
--

CREATE UNIQUE INDEX "UX_Users_UserName" ON login."Users" USING btree ("UserName");


-- Completed on 2026-08-29 12:51:24

--
-- PostgreSQL database dump complete
--

CREATE ROLE "RajaniSuppliers" WITH
  LOGIN
  SUPERUSER
  PASSWORD 'Admin@123';
