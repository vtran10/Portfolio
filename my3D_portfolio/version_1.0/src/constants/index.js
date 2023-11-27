
import {
  ai,
  nait,
  pcl,
  champ,
  stantec,
  nike,
  mobile,
  pokemon,
  starteddb,
  studygif,
  backend,
  web,
  javascript,
  html,
  css,
  reactjs,
  tailwind,
  nodejs,
  mongodb,
  git,
  figma,
  docker,
  threejs,} from "../assets";

export const navLinks = [
  {
    id: "about",
    title: "About",
  },
  {
    id: "work",
    title: "Work",
  },
  {
    id: "contact",
    title: "Contact",
  },
];

const services = [
  {
    title: "Web Developer",
    icon: web,
  },
  {
    title: "React Native Developer",
    icon: mobile,
  },
  {
    title: "Backend Developer",
    icon: backend,
  },
  {
    title: "Python Developer",
    icon: ai,
  },
];

const technologies = [
  {
    name: "HTML 5",
    icon: html,
  },
  {
    name: "CSS 3",
    icon: css,
  },
  {
    name: "JavaScript",
    icon: javascript,
  },
  {
    name: "Three JS",
    icon: threejs,
  },
  {
    name: "React JS",
    icon: reactjs,
  },
  {
    name: "Node JS",
    icon: nodejs,
  },
  {
    name: "Tailwind CSS",
    icon: tailwind,
  },
  {
    name: "MongoDB",
    icon: mongodb,
  },
  {
    name: "Git",
    icon: git,
  },
  {
    name: "Figma",
    icon: figma,
  },
  {
    name: "Docker",
    icon: docker,
  },
];

const experiences = [
  {
    title: "Graduate Student",
    company_name: "Northern Alberta Inistiute of Technology",
    icon: nait,
    iconBg: "#383E56",
    date: "Jan 2019 - Dec 2022",
    points: [
      "Major in Systems Administration",
      "In my role as a System Administrator, I acquired and refined skills focused on the implementation and upkeep of data infrastructure. I am adept at delivering IT solutions that contribute significant value to an organization.",
      "GPA 3.8 / 4.0",
      "Deans Honor Roll 2019-2022",
      "Scholarship Awardee for Albert St.George of England Society Founder's Bursary",
      "Scholorship Awardee for NAITSA General Award of Excellence",
    ],
  },
  {
    title: "Systems Administrator",
    company_name: "PCL Construction (Internship)",
    icon: pcl,
    iconBg: "#E6DEDD",
    date: "Jan 2021 - Dec 2021",
    points: [
      "Responsible for system deployments for userbase.",
      "Troubleshooting and resolving hardware, software and network issues.",
      "Setup, test and deploy Sensera Security Cameras.",
      "Trailer office setup and troubleshooting equipment from router and switches to printer and end user devices.",
    ],
  },
  {
    title: "IT Support Analyst",
    company_name: "Champion Petfoods (Internship)",
    icon: champ,
    iconBg: "#383E56",
    date: "April 2022 - Aug 2022",
    points: [
      "Manage workstations and IOT devices for both local and internation offices.",
      "Managed and supported IT infrastrure for both corporate and manufacturing sites.",
      "Troubleshot and resolved Boardroom AV issues.",
      "Collaborated with different teams to resolve network outages and meet organizational goals.",
    ],
  },
  {
    title: "Desktop Support",
    company_name: "Stantec Consulting",
    icon: stantec,
    iconBg: "#E6DEDD",
    date: "Nov 2022 - Present",
    points: [
      "Respsonsible for maintenance and setup of all boardrooms and collaboration areas for corporate office.",
      "Deployment and maintenance of hardware from imaging devices with our pxe imaging process, sofware deployment thruogh SCCM, and mobile devices with our Azure/Intune process.",
      "Collaborated with server and network teams to troublshoot network outages that affected local and national offices.",
      "Managing hardware inventory for numerous Business Units, including procurement of new hardware and software.",
      "Managing multiple projects and tasks at once to meet deadline for quartly, semi-anual and yearly organizational goals.",
    ],
  },
];

const testimonials = [
  {
    testimonial:
      "Victor's ability to think outside the box and find creative solutions to challenges is commendable. His innovative approach has significantly contributed to the success of our projects",
    name: "Captian America",
    designation: "Leader",
    company: "The Avengers",
    image:
      "https://cdn.britannica.com/30/182830-050-96F2ED76/Chris-Evans-title-character-Joe-Johnston-Captain.jpg",
  },
  {
    testimonial:
      "Victor's project management skills are outstanding. His organized approach, attention to timelines, and ability to juggle multiple tasks make him an essential part of our project teams.",
    name: "Bruce & Hulk",
    designation: "Strongest Avenger",
    company: "The Avengers",
    image:
      "https://www.looper.com/img/gallery/the-ending-of-the-incredible-hulk-explained/intro-1615571972.jpg",
  },
  {
    testimonial:
      "Victor is not just an individual contributor; he is a true team player. His collaborative spirit and willingness to support others enhance the overall dynamic of our workplace.",
    name: "Phil Coulson",
    designation: "Agent",
    company: "Shield",
    image:
      "https://oyster.ignimgs.com/mediawiki/apis.ign.com/the-avengers/7/73/The-Avengers-Agent-Coulson.jpg",
  },
];

const projects = [
  {
    name: "Nike",
    description:
      "Web-based platform that allows users to search our inventory of super unique and stylish selections of shoes.",
    tags: [
      {
        name: "react",
        color: "blue-text-gradient",
      },
      {
        name: "tailwind",
        color: "pink-text-gradient",
      },
    ],
    image: nike,
    source_code_link: "https://github.com/",
  },
  {
    name: ".Net Webapp",
    description:
      "A webapp that allows users to search and perform CRUD operations on a database of programs and courses.",
    tags: [
      {
        name: ".net core",
        color: "blue-text-gradient",
      },
      {
        name: "razor pages",
        color: "green-text-gradient",
      },
      {
        name: "ms entity framework",
        color: "pink-text-gradient",
      },
    ],
    image: starteddb,
    source_code_link: "https://github.com/",
  },
  {
    name: "Pokemon Api (Under Construction)",
    description:
      "A python based program that calls and parse the json data into python datasets",
    tags: [
      {
        name: "react",
        color: "blue-text-gradient",
      },
      {
        name: "api",
        color: "green-text-gradient",
      },
      {
        name: "python",
        color: "pink-text-gradient",
      },
    ],
    image: pokemon,
    source_code_link: "https://github.com/",
  },
  {
    name: "I'm currently working on",
    description:
      "Learning Angular, Alpine, HTMX, Typescript and more to become a full stack developer",
    tags: [
      {
        name: "angular",
        color: "blue-text-gradient",
      },
      {
        name: "alpine",
        color: "green-text-gradient",
      },
      {
        name: "htmx",
        color: "pink-text-gradient",
      },
      {
        name: "typescript",
        color: "blue-text-gradient",
      },
    ],
    image: studygif,
    source_code_link: "https://github.com/",
  },
];

export { services, technologies, experiences, testimonials, projects };
