PGDMP     8            
        {            control    15.2    15.2 +    &           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            '           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            (           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            )           1262    17973    control    DATABASE     |   CREATE DATABASE control WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Ukraine.1251';
    DROP DATABASE control;
                postgres    false            �            1259    18038    climate_control    TABLE     �   CREATE TABLE public.climate_control (
    id integer NOT NULL,
    list_id integer,
    "timestamp" timestamp without time zone NOT NULL,
    temperature real NOT NULL,
    humidity real NOT NULL
);
 #   DROP TABLE public.climate_control;
       public         heap    postgres    false            �            1259    18037    climate_control_id_seq    SEQUENCE     �   CREATE SEQUENCE public.climate_control_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.climate_control_id_seq;
       public          postgres    false    217            *           0    0    climate_control_id_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.climate_control_id_seq OWNED BY public.climate_control.id;
          public          postgres    false    216            �            1259    18064    energy_management    TABLE     �   CREATE TABLE public.energy_management (
    energy_id integer NOT NULL,
    list_id integer,
    electricity boolean NOT NULL,
    power_consumption real NOT NULL,
    energy_consumed timestamp without time zone NOT NULL
);
 %   DROP TABLE public.energy_management;
       public         heap    postgres    false            �            1259    18063    energy_management_energy_id_seq    SEQUENCE     �   CREATE SEQUENCE public.energy_management_energy_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 6   DROP SEQUENCE public.energy_management_energy_id_seq;
       public          postgres    false    221            +           0    0    energy_management_energy_id_seq    SEQUENCE OWNED BY     c   ALTER SEQUENCE public.energy_management_energy_id_seq OWNED BY public.energy_management.energy_id;
          public          postgres    false    220            �            1259    18076    greenhouse_monitoring    TABLE     �  CREATE TABLE public.greenhouse_monitoring (
    report_id integer NOT NULL,
    list_id integer,
    timestamp_information timestamp without time zone NOT NULL,
    temperature_information real NOT NULL,
    humidity_information real NOT NULL,
    plant_type_information character varying(255) NOT NULL,
    watering_schedule_information jsonb NOT NULL,
    soil_moisture_level_information real NOT NULL,
    irrigation_time_information timestamp without time zone NOT NULL,
    irrigation_volume_information real NOT NULL,
    power_consumption_information real NOT NULL,
    energy_consumed_information timestamp without time zone NOT NULL
);
 )   DROP TABLE public.greenhouse_monitoring;
       public         heap    postgres    false            �            1259    18075 #   greenhouse_monitoring_report_id_seq    SEQUENCE     �   CREATE SEQUENCE public.greenhouse_monitoring_report_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 :   DROP SEQUENCE public.greenhouse_monitoring_report_id_seq;
       public          postgres    false    223            ,           0    0 #   greenhouse_monitoring_report_id_seq    SEQUENCE OWNED BY     k   ALTER SEQUENCE public.greenhouse_monitoring_report_id_seq OWNED BY public.greenhouse_monitoring.report_id;
          public          postgres    false    222            �            1259    18031    list    TABLE     f   CREATE TABLE public.list (
    list_id integer NOT NULL,
    name_of_culture character varying(25)
);
    DROP TABLE public.list;
       public         heap    postgres    false            �            1259    18030    list_list_id_seq    SEQUENCE     �   CREATE SEQUENCE public.list_list_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.list_list_id_seq;
       public          postgres    false    215            -           0    0    list_list_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.list_list_id_seq OWNED BY public.list.list_id;
          public          postgres    false    214            �            1259    18050    watering_schedule    TABLE     =  CREATE TABLE public.watering_schedule (
    watering_id integer NOT NULL,
    list_id integer,
    plant_type character varying(255) NOT NULL,
    watering_schedule jsonb NOT NULL,
    soil_moisture_level real NOT NULL,
    irrigation_time timestamp without time zone NOT NULL,
    irrigation_volume real NOT NULL
);
 %   DROP TABLE public.watering_schedule;
       public         heap    postgres    false            �            1259    18049 !   watering_schedule_watering_id_seq    SEQUENCE     �   CREATE SEQUENCE public.watering_schedule_watering_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public.watering_schedule_watering_id_seq;
       public          postgres    false    219            .           0    0 !   watering_schedule_watering_id_seq    SEQUENCE OWNED BY     g   ALTER SEQUENCE public.watering_schedule_watering_id_seq OWNED BY public.watering_schedule.watering_id;
          public          postgres    false    218            z           2604    18041    climate_control id    DEFAULT     x   ALTER TABLE ONLY public.climate_control ALTER COLUMN id SET DEFAULT nextval('public.climate_control_id_seq'::regclass);
 A   ALTER TABLE public.climate_control ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    217    216    217            |           2604    18067    energy_management energy_id    DEFAULT     �   ALTER TABLE ONLY public.energy_management ALTER COLUMN energy_id SET DEFAULT nextval('public.energy_management_energy_id_seq'::regclass);
 J   ALTER TABLE public.energy_management ALTER COLUMN energy_id DROP DEFAULT;
       public          postgres    false    220    221    221            }           2604    18079    greenhouse_monitoring report_id    DEFAULT     �   ALTER TABLE ONLY public.greenhouse_monitoring ALTER COLUMN report_id SET DEFAULT nextval('public.greenhouse_monitoring_report_id_seq'::regclass);
 N   ALTER TABLE public.greenhouse_monitoring ALTER COLUMN report_id DROP DEFAULT;
       public          postgres    false    222    223    223            y           2604    18034    list list_id    DEFAULT     l   ALTER TABLE ONLY public.list ALTER COLUMN list_id SET DEFAULT nextval('public.list_list_id_seq'::regclass);
 ;   ALTER TABLE public.list ALTER COLUMN list_id DROP DEFAULT;
       public          postgres    false    214    215    215            {           2604    18053    watering_schedule watering_id    DEFAULT     �   ALTER TABLE ONLY public.watering_schedule ALTER COLUMN watering_id SET DEFAULT nextval('public.watering_schedule_watering_id_seq'::regclass);
 L   ALTER TABLE public.watering_schedule ALTER COLUMN watering_id DROP DEFAULT;
       public          postgres    false    219    218    219                      0    18038    climate_control 
   TABLE DATA           Z   COPY public.climate_control (id, list_id, "timestamp", temperature, humidity) FROM stdin;
    public          postgres    false    217   �7       !          0    18064    energy_management 
   TABLE DATA           p   COPY public.energy_management (energy_id, list_id, electricity, power_consumption, energy_consumed) FROM stdin;
    public          postgres    false    221   �7       #          0    18076    greenhouse_monitoring 
   TABLE DATA           Y  COPY public.greenhouse_monitoring (report_id, list_id, timestamp_information, temperature_information, humidity_information, plant_type_information, watering_schedule_information, soil_moisture_level_information, irrigation_time_information, irrigation_volume_information, power_consumption_information, energy_consumed_information) FROM stdin;
    public          postgres    false    223   8                 0    18031    list 
   TABLE DATA           8   COPY public.list (list_id, name_of_culture) FROM stdin;
    public          postgres    false    215   08                 0    18050    watering_schedule 
   TABLE DATA           �   COPY public.watering_schedule (watering_id, list_id, plant_type, watering_schedule, soil_moisture_level, irrigation_time, irrigation_volume) FROM stdin;
    public          postgres    false    219   g8       /           0    0    climate_control_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.climate_control_id_seq', 1, false);
          public          postgres    false    216            0           0    0    energy_management_energy_id_seq    SEQUENCE SET     N   SELECT pg_catalog.setval('public.energy_management_energy_id_seq', 1, false);
          public          postgres    false    220            1           0    0 #   greenhouse_monitoring_report_id_seq    SEQUENCE SET     R   SELECT pg_catalog.setval('public.greenhouse_monitoring_report_id_seq', 1, false);
          public          postgres    false    222            2           0    0    list_list_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.list_list_id_seq', 1, false);
          public          postgres    false    214            3           0    0 !   watering_schedule_watering_id_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public.watering_schedule_watering_id_seq', 1, false);
          public          postgres    false    218            �           2606    18043 $   climate_control climate_control_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public.climate_control
    ADD CONSTRAINT climate_control_pkey PRIMARY KEY (id);
 N   ALTER TABLE ONLY public.climate_control DROP CONSTRAINT climate_control_pkey;
       public            postgres    false    217            �           2606    18069 (   energy_management energy_management_pkey 
   CONSTRAINT     m   ALTER TABLE ONLY public.energy_management
    ADD CONSTRAINT energy_management_pkey PRIMARY KEY (energy_id);
 R   ALTER TABLE ONLY public.energy_management DROP CONSTRAINT energy_management_pkey;
       public            postgres    false    221            �           2606    18083 0   greenhouse_monitoring greenhouse_monitoring_pkey 
   CONSTRAINT     u   ALTER TABLE ONLY public.greenhouse_monitoring
    ADD CONSTRAINT greenhouse_monitoring_pkey PRIMARY KEY (report_id);
 Z   ALTER TABLE ONLY public.greenhouse_monitoring DROP CONSTRAINT greenhouse_monitoring_pkey;
       public            postgres    false    223                       2606    18036    list list_pkey 
   CONSTRAINT     Q   ALTER TABLE ONLY public.list
    ADD CONSTRAINT list_pkey PRIMARY KEY (list_id);
 8   ALTER TABLE ONLY public.list DROP CONSTRAINT list_pkey;
       public            postgres    false    215            �           2606    18057 (   watering_schedule watering_schedule_pkey 
   CONSTRAINT     o   ALTER TABLE ONLY public.watering_schedule
    ADD CONSTRAINT watering_schedule_pkey PRIMARY KEY (watering_id);
 R   ALTER TABLE ONLY public.watering_schedule DROP CONSTRAINT watering_schedule_pkey;
       public            postgres    false    219            �           2606    18044 ,   climate_control climate_control_list_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.climate_control
    ADD CONSTRAINT climate_control_list_id_fkey FOREIGN KEY (list_id) REFERENCES public.list(list_id);
 V   ALTER TABLE ONLY public.climate_control DROP CONSTRAINT climate_control_list_id_fkey;
       public          postgres    false    215    3199    217            �           2606    18070 0   energy_management energy_management_list_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.energy_management
    ADD CONSTRAINT energy_management_list_id_fkey FOREIGN KEY (list_id) REFERENCES public.list(list_id);
 Z   ALTER TABLE ONLY public.energy_management DROP CONSTRAINT energy_management_list_id_fkey;
       public          postgres    false    221    215    3199            �           2606    18084 8   greenhouse_monitoring greenhouse_monitoring_list_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.greenhouse_monitoring
    ADD CONSTRAINT greenhouse_monitoring_list_id_fkey FOREIGN KEY (list_id) REFERENCES public.list(list_id);
 b   ALTER TABLE ONLY public.greenhouse_monitoring DROP CONSTRAINT greenhouse_monitoring_list_id_fkey;
       public          postgres    false    223    215    3199            �           2606    18058 0   watering_schedule watering_schedule_list_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.watering_schedule
    ADD CONSTRAINT watering_schedule_list_id_fkey FOREIGN KEY (list_id) REFERENCES public.list(list_id);
 Z   ALTER TABLE ONLY public.watering_schedule DROP CONSTRAINT watering_schedule_list_id_fkey;
       public          postgres    false    3199    219    215                  x������ � �      !      x������ � �      #      x������ � �         '   x�3��/I,��2���1�9����r*�b���� ��	�            x������ � �     