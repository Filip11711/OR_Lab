--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0
-- Dumped by pg_dump version 16.0

-- Started on 2023-11-01 01:57:48

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
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 4869 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 219 (class 1259 OID 16477)
-- Name: igrac; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.igrac (
    ime character varying(50) NOT NULL,
    prezime character varying(50) NOT NULL,
    datum_rodenja date NOT NULL,
    id integer NOT NULL
);


ALTER TABLE public.igrac OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16435)
-- Name: natjecanje; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.natjecanje (
    id integer NOT NULL,
    naziv character varying(50) NOT NULL,
    godina integer NOT NULL,
    organizator character varying(50) NOT NULL,
    prvak character varying(50) NOT NULL,
    mjesto_finale character varying(50),
    sport character varying(50) NOT NULL
);


ALTER TABLE public.natjecanje OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16434)
-- Name: natjecanje_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.natjecanje_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.natjecanje_id_seq OWNER TO postgres;

--
-- TOC entry 4870 (class 0 OID 0)
-- Dependencies: 215
-- Name: natjecanje_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.natjecanje_id_seq OWNED BY public.natjecanje.id;


--
-- TOC entry 218 (class 1259 OID 16442)
-- Name: natjecatelj; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.natjecatelj (
    id integer NOT NULL,
    drzava character varying(50) NOT NULL,
    spol character varying(1) NOT NULL,
    natjecanjeid integer NOT NULL
);


ALTER TABLE public.natjecatelj OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16441)
-- Name: natjecatelj_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.natjecatelj_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.natjecatelj_id_seq OWNER TO postgres;

--
-- TOC entry 4871 (class 0 OID 0)
-- Dependencies: 217
-- Name: natjecatelj_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.natjecatelj_id_seq OWNED BY public.natjecatelj.id;


--
-- TOC entry 220 (class 1259 OID 16487)
-- Name: tim; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tim (
    naziv character varying(50) NOT NULL,
    osnovan date NOT NULL,
    trener character varying(50) NOT NULL,
    id integer NOT NULL
);


ALTER TABLE public.tim OWNER TO postgres;

--
-- TOC entry 4702 (class 2604 OID 16438)
-- Name: natjecanje id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.natjecanje ALTER COLUMN id SET DEFAULT nextval('public.natjecanje_id_seq'::regclass);


--
-- TOC entry 4703 (class 2604 OID 16445)
-- Name: natjecatelj id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.natjecatelj ALTER COLUMN id SET DEFAULT nextval('public.natjecatelj_id_seq'::regclass);


--
-- TOC entry 4862 (class 0 OID 16477)
-- Dependencies: 219
-- Data for Name: igrac; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Albane', 'Valenzuela', '1997-12-17', 71);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Linn', 'Grant', '1999-06-20', 72);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Pajaree', 'Anannarukarn', '1999-05-30', 73);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Carlota', 'Ciganda', '1990-06-01', 74);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Lindsey', 'Weaver-Wright', '1993-09-16', 75);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Leona', 'Maguire', '1994-11-30', 76);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Celine', 'Boutier', '1993-11-10', 77);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Furue', 'Ayaka', '2000-05-27', 78);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Fan', 'Zhendong', '1997-01-22', 79);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Jeoung', 'Youngsik', '1992-01-20', 80);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Lin', 'Youn-Ju', '2001-08-17', 81);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Darko', 'Jorgic', '1998-07-30', 82);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Hugo', 'Calderano', '1996-06-22', 83);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Dimitrij', 'Ovtcharov', '1988-09-02', 84);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Omar', 'Assar', '1991-07-22', 85);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Ma', 'Long', '1988-10-20', 86);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('An', 'Se Young', '2002-02-05', 87);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Nozomi', 'Okuhara', '1995-03-13', 88);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Chen', 'Yu Fei', '1998-03-01', 89);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Wang', 'Zhi Yi', '2000-04-29', 90);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Carolina', 'Marin', '1993-06-15', 91);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Tai', 'Tzu Ying', '1994-06-20', 92);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Gregoria Mariska', 'Tunjung', '1999-08-11', 93);
INSERT INTO public.igrac (ime, prezime, datum_rodenja, id) VALUES ('Akane', 'Yamaguchi', '1997-06-06', 94);


--
-- TOC entry 4859 (class 0 OID 16435)
-- Dependencies: 216
-- Data for Name: natjecanje; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (3, 'Prva Hrvatska nogometna liga', 2021, 'HNS', 'GNK Dinamo Zagreb', NULL, 'nogomet');
INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (4, 'Prva Hrvatska nogometna liga', 2020, 'HNS', 'GNK Dinamo Zagreb', NULL, 'nogomet');
INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (5, 'Prva Hrvatska nogometna liga', 2019, 'HNS', 'GNK Dinamo Zagreb', NULL, 'nogomet');
INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (6, 'Prva Hrvatska nogometna liga', 2018, 'HNS', 'GNK Dinamo Zagreb', NULL, 'nogomet');
INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (7, 'Prva Hrvatska nogometna liga', 2017, 'HNS', 'GNK Dinamo Zagreb', NULL, 'nogomet');
INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (8, 'Prva Hrvatska nogometna liga', 2016, 'HNS', 'HNK Rijeka', NULL, 'nogomet');
INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (2, 'SuperSport Hrvatska nogometna liga', 2022, 'HNS', 'GNK Dinamo Zagreb', NULL, 'nogomet');
INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (9, 'Bank of hope lpga match-play', 2023, 'LPGA', 'Pajaree Anannarukarn', 'Las Vegas', 'golf');
INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (10, 'Olimpijske igre - Final 8', 2020, 'MOO', 'Ma Long', 'Tokio', 'Stolni tenis');
INSERT INTO public.natjecanje (id, naziv, godina, organizator, prvak, mjesto_finale, sport) VALUES (11, 'World Championships - Final 8', 2023, 'BWF', 'An Se Young', 'Copenhagen', 'badminton');


--
-- TOC entry 4861 (class 0 OID 16442)
-- Dependencies: 218
-- Data for Name: natjecatelj; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (1, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (2, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (3, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (4, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (5, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (6, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (7, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (8, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (9, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (10, 'Hrvatska', 'M', 2);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (11, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (12, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (13, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (14, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (15, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (16, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (17, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (18, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (19, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (20, 'Hrvatska', 'M', 3);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (21, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (22, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (23, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (24, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (25, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (26, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (27, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (28, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (29, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (30, 'Hrvatska', 'M', 4);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (31, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (32, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (33, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (34, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (35, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (36, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (37, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (38, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (39, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (40, 'Hrvatska', 'M', 5);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (41, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (42, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (43, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (44, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (45, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (46, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (47, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (48, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (49, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (50, 'Hrvatska', 'M', 6);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (51, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (52, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (53, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (54, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (55, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (56, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (57, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (58, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (59, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (60, 'Hrvatska', 'M', 7);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (61, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (62, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (63, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (64, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (65, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (66, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (67, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (68, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (69, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (70, 'Hrvatska', 'M', 8);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (71, 'Švicarska', 'Ž', 9);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (72, 'Švedska', 'Ž', 9);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (73, 'Tajland', 'Ž', 9);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (74, 'Španjolska', 'Ž', 9);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (75, 'SAD', 'Ž', 9);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (76, 'Irska', 'Ž', 9);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (77, 'Francuska', 'Ž', 9);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (78, 'Japan', 'Ž', 9);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (79, 'Kina', 'M', 10);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (80, 'Južna Koreja', 'M', 10);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (81, 'Tajvan', 'M', 10);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (82, 'Slovenija', 'M', 10);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (83, 'Brazil', 'M', 10);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (84, 'Njemačka', 'M', 10);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (85, 'Egipat', 'M', 10);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (86, 'Kina', 'M', 10);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (87, 'Južna Koreja', 'Ž', 11);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (88, 'Japan', 'Ž', 11);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (89, 'Kina', 'Ž', 11);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (90, 'Kina', 'Ž', 11);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (91, 'Španjolska', 'Ž', 11);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (92, 'Tajvan', 'Ž', 11);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (93, 'Indonezija', 'Ž', 11);
INSERT INTO public.natjecatelj (id, drzava, spol, natjecanjeid) VALUES (94, 'Japan', 'Ž', 11);


--
-- TOC entry 4863 (class 0 OID 16487)
-- Dependencies: 220
-- Data for Name: tim; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('GNK Dinamo Zagreb', '1945-06-09', 'Ante Čačić', 1);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Hajduk Split', '1911-02-13', 'Ivan Leko', 2);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Rijeka', '1906-01-01', 'Sergej Jakirović', 3);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Osijek', '1947-02-27', 'Stjepan Tomas', 4);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Lokomotiva Zagreb', '1914-01-01', 'Silvijo Čabraja', 5);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Slaven Belupo', '1907-01-01', 'Zoran Zekić', 6);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Istra 1961', '1948-01-01', 'Gonzalo Garcia', 7);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Velika Gorica', '2009-01-01', 'Željko Sopić', 8);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Šibenik', '1932-12-01', 'Damir Čanadi', 9);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Varaždin', '2012-01-01', 'Mario Kovačević', 10);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('GNK Dinamo Zagreb', '1945-06-09', 'Ante Čačić', 11);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Hajduk Split', '1911-02-13', 'Valdas Dambrauskas', 12);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Rijeka', '1906-01-01', 'Goran Tomič', 13);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Osijek', '1947-02-27', 'Nenad Bjelica', 14);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Lokomotiva Zagreb', '1914-01-01', 'Silvijo Čabraja', 15);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Slaven Belupo', '1907-01-01', 'Zoran Zekić', 16);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Istra 1961', '1948-01-01', 'Gonzalo Garcia', 17);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Velika Gorica', '2009-01-01', 'Samir Toplak', 18);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Šibenik', '1932-12-01', 'Damir Čanadi', 19);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Hrvatski Dragovoljac', '1975-01-01', 'Dean Računica', 20);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('GNK Dinamo Zagreb', '1945-06-09', 'Damir Krznar', 21);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Hajduk Split', '1911-02-13', 'Paolo Tramezzani', 22);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Rijeka', '1906-01-01', 'Goran Tomić', 23);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Osijek', '1947-02-27', 'Nenad Bjelica', 24);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Lokomotiva Zagreb', '1914-01-01', 'Samir Toplak', 25);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Slaven Belupo', '1907-01-01', 'Tomislav Stipić', 26);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Istra 1961', '1948-01-01', 'Danijel Jumić', 27);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Velika Gorica', '2009-01-01', 'Siniša Oreščanin', 28);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Šibenik', '1932-12-01', 'Sergi Escobar', 29);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Varaždin', '2012-01-01', 'Zoran Kastel', 30);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('GNK Dinamo Zagreb', '1945-06-09', 'Zoran Mamić', 31);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Hajduk Split', '1911-02-13', 'Igor Tudor', 32);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Rijeka', '1906-01-01', 'Simon Rožman', 33);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Osijek', '1947-02-27', 'Ivica Kulešević', 34);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Lokomotiva Zagreb', '1914-01-01', 'Goran Tomić', 35);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Istra 1961', '1948-01-01', 'Ivan Prelec', 37);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Velika Gorica', '2009-01-01', 'Sergej Jakirović', 38);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Inter Zaprešić', '1929-06-25', 'Tomislav Ivković', 39);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Varaždin', '2012-01-01', 'Samir Toplak', 40);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Slaven Belupo', '1907-01-01', 'Tomislav Stipić', 36);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('GNK Dinamo Zagreb', '1945-06-09', 'Nenad Bjelica', 41);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Hajduk Split', '1911-02-13', 'Siniša Oreščanin', 42);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Rijeka', '1906-01-01', 'Igor Bišćan', 43);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Osijek', '1947-02-27', 'Dino Skledar', 44);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Lokomotiva Zagreb', '1914-01-01', 'Goran Tomić', 45);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Slaven Belupo', '1907-01-01', 'Ivica Sertić', 46);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Istra 1961', '1948-01-01', 'Igor Cvitanović', 47);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Velika Gorica', '2009-01-01', 'Sergej Jakirović', 48);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Inter Zaprešić', '1929-06-25', 'Samir Toplak', 49);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Rudeš', '1957-01-01', 'Tomislav Ivković', 50);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('GNK Dinamo Zagreb', '1945-06-09', 'Mario Cvitanović', 51);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Hajduk Split', '1911-02-13', 'Željko Kopić', 52);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Rijeka', '1906-01-01', 'Matjaž Kek', 53);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Osijek', '1947-02-27', 'Zoran Zekić', 54);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Lokomotiva Zagreb', '1914-01-01', 'Goran Tomić', 55);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Slaven Belupo', '1907-01-01', 'Tomislav Ivković', 56);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Istra 1961', '1948-01-01', 'Darko Raić-Sudar', 57);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Cibalia', '1919-01-01', 'Davor Rupnik', 58);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Inter Zaprešić', '1929-06-25', 'Samir Toplak', 59);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Rudeš', '1957-01-01', 'Jose Manuel Aira', 60);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('GNK Dinamo Zagreb', '1945-06-09', 'Ivajlo Petev', 61);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Hajduk Split', '1911-02-13', 'Joan Carrillo', 62);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Rijeka', '1906-01-01', 'Matjaž Kek', 63);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Osijek', '1947-02-27', 'Zoran Zekić', 64);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Lokomotiva Zagreb', '1914-01-01', 'Mario Tokić', 65);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Slaven Belupo', '1907-01-01', 'Željko Kopić', 66);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Istra 1961', '1948-01-01', 'Darko Raić-Sudar', 67);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('HNK Cibalia', '1919-01-01', 'Dražen Pernar', 68);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('NK Inter Zaprešić', '1929-06-25', 'Samir Toplak', 69);
INSERT INTO public.tim (naziv, osnovan, trener, id) VALUES ('RNK Split', '1912-04-16', 'Bruno Akrapović', 70);


--
-- TOC entry 4872 (class 0 OID 0)
-- Dependencies: 215
-- Name: natjecanje_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.natjecanje_id_seq', 11, true);


--
-- TOC entry 4873 (class 0 OID 0)
-- Dependencies: 217
-- Name: natjecatelj_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.natjecatelj_id_seq', 94, true);


--
-- TOC entry 4709 (class 2606 OID 16481)
-- Name: igrac igrac_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.igrac
    ADD CONSTRAINT igrac_pkey PRIMARY KEY (id);


--
-- TOC entry 4705 (class 2606 OID 16440)
-- Name: natjecanje natjecanje_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.natjecanje
    ADD CONSTRAINT natjecanje_pkey PRIMARY KEY (id);


--
-- TOC entry 4707 (class 2606 OID 16447)
-- Name: natjecatelj natjecatelj_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.natjecatelj
    ADD CONSTRAINT natjecatelj_pkey PRIMARY KEY (id);


--
-- TOC entry 4711 (class 2606 OID 16491)
-- Name: tim tim_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tim
    ADD CONSTRAINT tim_pkey PRIMARY KEY (id);


--
-- TOC entry 4713 (class 2606 OID 16482)
-- Name: igrac igrac_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.igrac
    ADD CONSTRAINT igrac_id_fkey FOREIGN KEY (id) REFERENCES public.natjecatelj(id);


--
-- TOC entry 4712 (class 2606 OID 16448)
-- Name: natjecatelj natjecatelj_natjecanjeid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.natjecatelj
    ADD CONSTRAINT natjecatelj_natjecanjeid_fkey FOREIGN KEY (natjecanjeid) REFERENCES public.natjecanje(id);


--
-- TOC entry 4714 (class 2606 OID 16492)
-- Name: tim tim_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tim
    ADD CONSTRAINT tim_id_fkey FOREIGN KEY (id) REFERENCES public.natjecatelj(id);


-- Completed on 2023-11-01 01:57:49

--
-- PostgreSQL database dump complete
--

